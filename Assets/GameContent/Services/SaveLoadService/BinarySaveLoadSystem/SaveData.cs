using System;
using System.Collections.Generic;
using GameContent.Entities;

namespace GameContent.Services.SaveLoadService.BinarySaveLoadSystem
{
    [Serializable]
    public class SaveData
    {
        public string LevelId;
        public List<SceneItem> SceneItems;
        public Dictionary<string, int> Inventory;
    }
}