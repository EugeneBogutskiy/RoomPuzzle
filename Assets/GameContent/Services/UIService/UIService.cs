using GameContent.Services.UIService.Abstract;
using GameContent.UI.UIScripts.GameMenuView.Abstract;
using UnityEngine;
using UniRx;
using Configs;
using System.Linq;

namespace GameContent.Services.UIService
{
    public class UIService : IUIService
    {
        private readonly GameObject _gameMenuView;
        private readonly UIConfig _uiConfig;
        
        private readonly ReactiveCommand<Unit> _saveCommand = new ReactiveCommand<Unit>();
        private readonly ReactiveCommand<Unit> _loadCommand = new ReactiveCommand<Unit>();
        private readonly ReactiveCommand<Unit> _exitCommand = new ReactiveCommand<Unit>();

        public IReactiveCommand<Unit> SaveCommand => _saveCommand;
        public IReactiveCommand<Unit> LoadCommand => _loadCommand;
        public IReactiveCommand<Unit> ExitCommand => _exitCommand;
        
        public UIService(UIConfig uiConfig, GameObject gameMenuView)
        {
            _uiConfig = uiConfig;
            _gameMenuView = gameMenuView;

            CreateView();
        }

        public GameObject OpenView(UIViewNames.VewNames viewId) 
        {
            var view = _uiConfig.viewsMap
                .Where(x => x.Key == viewId)
                .Where(x => x.Value != null)
                .Select(x => x.Value)
                .FirstOrDefault();

            if (view != null)
            {
                return GameObject.Instantiate(view);
            }
            else
            {
                Debug.LogError($"view with name {viewId} not found!");
                return null;
            }
        }

        private void CreateView()
        {
            var view = GameObject.Instantiate(_gameMenuView);
            var uiView = view.GetComponent<IGameMenuView>();

            uiView.Save.Subscribe(_ => _saveCommand.Execute(Unit.Default));
            uiView.Load.Subscribe(_ => _loadCommand.Execute(Unit.Default));
            uiView.Exit.Subscribe(_ => _exitCommand.Execute(Unit.Default));
        }
    }
}