namespace GameContent.InventorySystem.InventorySystemWithSlots.Abstract
{
    public interface IInventoryItemState
    {
        int Amount { get; set; }
        bool IsEquipped { get; set; }
    }
}