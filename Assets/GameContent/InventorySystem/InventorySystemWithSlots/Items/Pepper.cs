using GameContent.InventorySystem.InventorySystemWithSlots.Abstract;

namespace GameContent.InventorySystem.InventorySystemWithSlots.Items
{
    public class Pepper : IInventoryItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public InventoryType InventoryType => InventoryType.Two;
        
        public Pepper(IInventoryItemInfo info)
        {
            Info = info;
            State = new InventoryItemState();
        }
    }
}