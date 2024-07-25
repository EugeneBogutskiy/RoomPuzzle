namespace GameContent.InventorySystem.SimpleInventorySystem.Abstract
{
    public interface IItemObject
    {
        InventoryItemData ReferenceItem { get; }

        void PickUpItem();
    }
}