using GameContent.InventorySystem.InventorySystemWithSlots.Abstract;

namespace GameContent.InventorySystem.InventorySystemWithSlots.Items
{
    public class Apple : IInventoryItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public InventoryType InventoryType => InventoryType.One;

        public Apple(IInventoryItemInfo info)
        {
            Info = info;
            State = new InventoryItemState();
        }
    }
}