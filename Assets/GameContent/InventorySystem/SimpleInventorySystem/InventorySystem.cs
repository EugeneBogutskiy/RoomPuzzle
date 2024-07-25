using System.Collections.Generic;
using GameContent.InventorySystem.SimpleInventorySystem.Abstract;
using UniRx;
using UnityEngine;

namespace GameContent.InventorySystem.SimpleInventorySystem
{
    public class InventorySystem : IInventorySystem
    {
        private readonly ReactiveCommand<Unit> _onUpdateInventory;
        
        public Dictionary<InventoryItemData, InventoryItem> InventoryItems { get; }
        public List<InventoryItem> Inventory { get; private set; }

        public IReactiveCommand<Unit> OnUpdateInventory => _onUpdateInventory;

        public InventorySystem(GameObject inventoryView)
        {
            _onUpdateInventory = new ReactiveCommand<Unit>();
            
            InventoryItems = new Dictionary<InventoryItemData, InventoryItem>();
            Inventory = new List<InventoryItem>();

            GameObject.Instantiate(inventoryView);
        }

        public void Add(InventoryItemData referenceData)
        {
            if (InventoryItems.TryGetValue(referenceData, out var value))
            {
                value.AddToStack();
            }
            else
            {
                InventoryItem item = new InventoryItem(referenceData);
                Inventory.Add(item);
                AddToInventoryItems(referenceData, item);
            }

            _onUpdateInventory.Execute(Unit.Default);
        }

        public void Remove(InventoryItemData referenceData)
        {
            if (InventoryItems.TryGetValue(referenceData, out var value))
            {
                value.RemoveFromStack();

                if (value.StackSize == 0)
                {
                    Inventory.Remove(value);
                    InventoryItems.Remove(referenceData);
                }
            }

            _onUpdateInventory.Execute(Unit.Default);
        }

        public void AddToInventoryItems(InventoryItemData itemData, InventoryItem item)
        {
            InventoryItems.Add(itemData, item);
        }

        public InventoryItem GetInventoryItem(InventoryItemData referenceData)
        {
            if (InventoryItems.TryGetValue(referenceData, out var value))
            {
                return value;
            }

            return null;
        }

        // Set inventory from save data
        public void SetInventory(List<InventoryItem> inventoryItems)
        {
            Inventory = inventoryItems;

            _onUpdateInventory.Execute(Unit.Default);
        }
    }
}