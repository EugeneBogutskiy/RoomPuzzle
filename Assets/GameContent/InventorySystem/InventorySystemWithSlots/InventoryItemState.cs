using System;
using GameContent.InventorySystem.InventorySystemWithSlots.Abstract;

namespace GameContent.InventorySystem.InventorySystemWithSlots
{
    [Serializable]
    public class InventoryItemState : IInventoryItemState
    {
        public int amount;
        public bool isEquipped;
        
        public int Amount { get => amount; set => amount = value; }
        public bool IsEquipped { get => isEquipped; set => isEquipped = value; }

        public InventoryItemState()
        {
            amount = 0;
            isEquipped = false;
        }
    }
}