using UnityEngine;

namespace GameContent.InventorySystem.SimpleInventorySystem
{
    [CreateAssetMenu(menuName = "Evgenoid/InventoryItemData", fileName = "InventoryItem")]
    public class InventoryItemData : ScriptableObject
    {
        [Header("Id must be the same as Addressable asset name!!!")]
        public string id;
        [Space(20)]
        public string displayName;
        public Sprite icon;
        public GameObject prefab;
    }
}