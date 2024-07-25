using GameContent.Services.SaveLoadService.Abstract;
using GameContent.Services.SaveLoadService.BinarySaveLoadSystem;

namespace GameContent.Services.SaveLoadService.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly ISaveLoadSystem _saveLoadSystem;

        public SaveLoadService(ISaveLoadSystem saveLoadSystem)
        {
            _saveLoadSystem = saveLoadSystem;
        }

        public void Save(SaveData data)
        {
            _saveLoadSystem.Save(data);
        }

        public SaveData Load()
        {
            return _saveLoadSystem.Load();
        }
    }
}