namespace GameContent.InventorySystem.InventorySystemWithSlots.Abstract
{
    public interface IInventory
    {
        int Capacity { get; set; }
        bool IsFull { get; }

        IInventoryItem GetItem(InventoryType inventoryType);
        IInventoryItem[] GetAllItems(InventoryType inventoryType);
        IInventoryItem[] GetAllItems();
        IInventoryItem[] GetEquippedItems();
        int GetItemAmount(InventoryType inventoryType);

        bool TryAdd(object sender, IInventoryItem item);
        void Remove(object sender, InventoryType inventoryType, int amount = 1);
        bool HasItem(InventoryType inventoryType, out IInventoryItem inventoryItem);
    }
}