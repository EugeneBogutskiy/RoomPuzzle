using System.Collections.Generic;
using UniRx;

namespace GameContent.InventorySystem.SimpleInventorySystem.Abstract
{
    public interface IInventorySystem
    {
        void Add(InventoryItemData referenceData);
        void Remove(InventoryItemData referenceData);
        void SetInventory(List<InventoryItem> inventoryItems);
        void AddToInventoryItems(InventoryItemData itemData, InventoryItem item);
        InventoryItem GetInventoryItem(InventoryItemData referenceData);
        
        List<InventoryItem> Inventory { get; }
        Dictionary<InventoryItemData, InventoryItem> InventoryItems { get; }
        
        IReactiveCommand<Unit> OnUpdateInventory { get; }
    }
}