using GameContent.InventorySystem.InventorySystemWithSlots.Abstract;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameContent.InventorySystem.InventorySystemWithSlots.UI
{
    public class UIInventorySlot : UISlot
    {
        private UIInventory _uiInventory;

        [SerializeField] private UIInventoryItem _uiInventoryItem;
        
        public IInventorySlot Slot { get; private set; }

        private void Awake()
        {
            _uiInventory = GetComponentInParent<UIInventory>();
        }

        public void SetSlot(IInventorySlot newSlot)
        {
            Slot = newSlot;
        }
        
        public override void OnDrop(PointerEventData eventData)
        {
            var otherItemUi = eventData.pointerDrag.GetComponent<UIInventoryItem>();
            var otherSlotUI = otherItemUi.GetComponentInParent<UIInventorySlot>();
            var otherSlot = otherSlotUI.Slot;
            var inventory = _uiInventory.Inventory;
            
            inventory.MoveFromSlotToSlot(this, otherSlot, Slot);
            
            Refresh();
            otherSlotUI.Refresh();
        }

        private void Refresh()
        {
            if (Slot != null)
            {
                _uiInventoryItem.Refresh(Slot);
            }
        }
    }
}