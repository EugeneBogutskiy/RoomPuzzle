namespace GameContent.InventorySystem.InventorySystemWithSlots.Abstract
{
    public interface IInventoryItem
    {
        IInventoryItemInfo Info { get; }
        IInventoryItemState State { get; }
        InventoryType InventoryType { get; }
    }

    public enum InventoryType
    {
        One,
        Two,
        Three
    }
}