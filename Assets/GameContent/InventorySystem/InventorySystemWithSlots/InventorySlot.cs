using GameContent.InventorySystem.InventorySystemWithSlots.Abstract;

namespace GameContent.InventorySystem.InventorySystemWithSlots
{
    public class InventorySlot : IInventorySlot
    {
        public bool IsFull => !IsEmpty && Amount == Capacity;
        public bool IsEmpty => Item is null;
        public IInventoryItem Item { get; private set; }
        public int Amount => IsEmpty ? 0 : Item.State.Amount;
        public int Capacity { get; private set; }
        
        public void SetItem(IInventoryItem item)
        {
            if (! IsEmpty)
            {
                return;
            }
            
            Item = item;
            Capacity = item.Info.MaxItemsInInventorySlot;
        }

        public void Clear()
        {
            if (IsEmpty)
            {
                return;;
            }
            
            Item.State.Amount = 0;
            Item = null;
        }
    }
}