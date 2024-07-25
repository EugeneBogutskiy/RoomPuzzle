using System.Collections.Generic;
using System.Linq;
using Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameContent.Entities;
using GameContent.InventorySystem.SimpleInventorySystem;
using GameContent.Services.CameraControllerService.Abstract;
using GameContent.Services.InventoryService.Abstract;
using GameContent.Services.SaveLoadService.Abstract;
using GameContent.Services.SaveLoadService.BinarySaveLoadSystem;
using GameContent.Services.SceneService.Abstract;
using GameContent.Services.UIService.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

namespace GameContent.Services.SceneService
{
    public class SceneService : ISceneService
    {
        public IReadOnlyReactiveProperty<float> LoadingPercentage => _loadingPercentageProgress;

        private ISaveLoadService _saveLoadService;
        private IInventoryService _inventoryService;
        private IUIService _uiService;
        private ICameraControllerService _cameraControllerService;
        private string _sceneId;
        private float minLoadingTime = 3f;
        private bool canUnloadScene;
        private bool sceneLoaded;

        private ReactiveProperty<float> _loadingPercentageProgress = new ReactiveProperty<float>();

        private List<InteractWithStateObjectBase> _sceneInteractableObjects;

        public SceneService()
        {
            Initialize();
        }

        private void Initialize()
        {
            MessageBroker.Default.Receive<ISaveLoadService>().Subscribe(OnServiceReceived);
            MessageBroker.Default.Receive<IUIService>().Subscribe(OnUIServiceReceived);
            MessageBroker.Default.Receive<IInventoryService>().Subscribe(OnInventoryServiceReceived);
            MessageBroker.Default.Receive<ICameraControllerService>().Subscribe(OnCameraServiceReceived);

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        public void SaveScene()
        {
            var saveData = new SaveData()
            {
                SceneItems = new List<SceneItem>(),
                LevelId = _sceneId,
                Inventory = new Dictionary<string, int>()
            };

            // Fill SaveData with scene items
            foreach (var interactableObject in _sceneInteractableObjects)
            {
                var interactableData = interactableObject.GetState();

                saveData.SceneItems.Add(new SceneItem()
                {
                    Id = interactableData.Id,
                    Position = interactableData.Position,
                    Rotation = interactableData.Rotation
                });
            }

            // Fill SaveData with Inventory Items
            foreach (var inventoryItem in _inventoryService.InventorySystem.Inventory)
            {
                var itemId = inventoryItem.Data.id;

                saveData.Inventory.Add(itemId, inventoryItem.StackSize);
            }

            _saveLoadService.Save(saveData);
        }

        public async void LoadFromSaveData()
        {
            // Запускаем перезагрузку игры, загружаем данные

            var saveData = _saveLoadService.Load();

            // Set items in scene
            foreach (var sceneItem in saveData.SceneItems)
            {
                foreach (var interactableObject in _sceneInteractableObjects)
                {
                    if (interactableObject.Id == sceneItem.Id)
                    {
                        interactableObject.SetState(new InteractableData()
                        {
                            Position = sceneItem.Position,
                            Rotation = sceneItem.Rotation
                        });
                    }
                }
            }

            // Set inventory
            var inventoryItems = new List<InventoryItem>();

            foreach (var inventoryItem in saveData.Inventory)
            {
                var itemData = await LoadItemDataAsync(inventoryItem);
                var item = new InventoryItem(itemData);
                item.RemoveFromStack(int.MaxValue);
                item.AddToStack(inventoryItem.Value);
                inventoryItems.Add(item);

                if (_inventoryService.InventorySystem.InventoryItems.ContainsKey(itemData))
                    continue;

                _inventoryService.InventorySystem.AddToInventoryItems(itemData, item);
            }

            _inventoryService.InventorySystem.SetInventory(inventoryItems);
        }

        public async void LoadScene(string sceneId)
        {
            //стартуем экран загрузки
            //берем данные из сохранения если они есть
            //применяем данные к сцене и предметам
            //берем данные игрока
            //применяем данные и инвентарь к игроку
            //запускаем сцену
            //убираем экран загрузки

            StartLoadingScreen();

            if (canUnloadScene)
            {
                UnloadCurrentScene();
            }

            await LoadSceneAsync(sceneId);
            PerformCameraAfterLoadSceneBehaviour();

            canUnloadScene = true;
        }

        private void OnServiceReceived(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        private void OnUIServiceReceived(IUIService uiService)
        {
            _uiService = uiService;
            _uiService.SaveCommand.Subscribe(_ => SaveScene());
            _uiService.LoadCommand.Subscribe(_ => LoadFromSaveData());
        }

        private void OnInventoryServiceReceived(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        private void OnCameraServiceReceived(ICameraControllerService cameraControllerService)
        {
            _cameraControllerService = cameraControllerService;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            _sceneId = scene.name;

            _sceneInteractableObjects = new List<InteractWithStateObjectBase>();
            _sceneInteractableObjects = GameObject.FindObjectsOfType<InteractWithStateObjectBase>().ToList();

            //в самом начале игры загружаем сохраненные данные, применяем инвентарь
            //LoadFromSaveData();
        }

        private void OnSceneUnloaded(Scene scene)
        {

        }

        private async UniTask<InventoryItemData> LoadItemDataAsync(KeyValuePair<string, int> inventoryItem)
        {
            var asset = await Addressables.LoadAssetAsync<InventoryItemData>(inventoryItem.Key);
            return asset;
        }

        private void StartLoadingScreen()
        {
            var loadingScreen = _uiService.OpenView(UIViewNames.VewNames.LoadingScreen);

            if (loadingScreen != null)
            {
                var screenView = loadingScreen.GetComponent<LoadingScreen>();
                screenView.Init(this as ISceneService);
            }
        }

        private void UnloadCurrentScene()
        {
            SceneManager.UnloadSceneAsync(_sceneId);
        }

        private async UniTask LoadSceneAsync(string sceneName)
        {
            var asyncOperation = UnityEngine.SceneManagement.SceneManager
                .LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            var timer = Observable.Timer(System.TimeSpan.FromSeconds(minLoadingTime));
            timer.Subscribe(_ => sceneLoaded = true);

            var startTime = Time.time;

            while (!asyncOperation.isDone || !sceneLoaded)
            {
                var elapsedTime = Time.time - startTime;

                _loadingPercentageProgress.Value = Mathf.Clamp01(elapsedTime / minLoadingTime);

                // If the operation progress is 0.9, it means the scene is loaded but not activated
                //if (asyncOperation.progress >= 0.9f)
                //{
                //    // Set the progress to 1
                //    _loadingPercentageProgress.Value = 1f;
                //}

                // Wait for the next frame
                await UniTask.Yield();
            }
        }
        private void PerformCameraAfterLoadSceneBehaviour()
        {
            _cameraControllerService.Camera.orthographicSize = 50f;

            DOVirtual.Float(_cameraControllerService.Camera.orthographicSize, 7f,
                2f, f => _cameraControllerService.Camera.orthographicSize = f);
        }
    }
}