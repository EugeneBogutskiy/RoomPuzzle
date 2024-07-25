using GameContent.InventorySystem.SimpleInventorySystem.Abstract;
using UnityEngine;

namespace GameContent.InventorySystem.SimpleInventorySystem
{
    public class ItemObject : MonoBehaviour, IItemObject
    {
        [SerializeField]
        private InventoryItemData _referenceItem;

        public InventoryItemData ReferenceItem => _referenceItem;
        
        public void PickUpItem()
        {
            Debug.Log($"{_referenceItem.id} has picked up!");
            Destroy(gameObject);
        }
    }
}