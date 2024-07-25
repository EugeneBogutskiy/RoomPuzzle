using GameContent.Services.SaveLoadService.BinarySaveLoadSystem;

namespace GameContent.Services.SaveLoadService.Abstract
{
    public interface ISaveLoadService
    {
        void Save(SaveData data);
        SaveData Load();
    }
}