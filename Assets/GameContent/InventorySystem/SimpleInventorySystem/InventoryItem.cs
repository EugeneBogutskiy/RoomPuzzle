using System;
using GameContent.InventorySystem.SimpleInventorySystem.Abstract;

namespace GameContent.InventorySystem.SimpleInventorySystem
{
    [Serializable]
    public class InventoryItem : IInventoryItem
    {
        [field: NonSerialized]
        public InventoryItemData Data { get; private set; }
        public int StackSize { get; private set; }
        
        public InventoryItem(InventoryItemData source)
        {
            Data = source;
            AddToStack();
        }
        
        public void AddToStack(int count = 1)
        {
            StackSize += count;
        }

        public void RemoveFromStack(int count = 1)
        {
            var tempValue = StackSize -= count;

            StackSize = tempValue <= 0 ? StackSize = 0 : StackSize -= count;
        }
    }
}