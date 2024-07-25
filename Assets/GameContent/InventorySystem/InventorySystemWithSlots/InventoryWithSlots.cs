using System;
using System.Collections.Generic;
using System.Linq;
using GameContent.InventorySystem.InventorySystemWithSlots.Abstract;
using UnityEngine;

namespace GameContent.InventorySystem.InventorySystemWithSlots
{
    public class InventoryWithSlots : IInventory
    {
        private List<IInventorySlot> _slots;

        public event Action<object, IInventoryItem, int> OnInventoryItemAdded;
        public event Action<object, InventoryType, int> OnInventoryItemRemoved;
        public event Action<object> OnInventoryStateChangedEvent; 

        public int Capacity { get; set; }
        public bool IsFull => _slots.All(x => x.IsFull);

        public InventoryWithSlots(int capacity)
        {
            Capacity = capacity;

            _slots = new List<IInventorySlot>(capacity);
            
            for (var i = 0; i < capacity; i++)
            {
                _slots.Add(new InventorySlot());
            }
        }
        
        public IInventoryItem GetItem(InventoryType inventoryType)
        {
            return _slots.Where(x => x.Item.InventoryType == inventoryType)
                .Select(x => x.Item)
                .FirstOrDefault();
        }

        public IInventoryItem[] GetAllItems(InventoryType inventoryType)
        {
            return _slots.Where(x => x.Item.InventoryType == inventoryType)
                .Select(x => x.Item)
                .ToArray();
        }

        public IInventoryItem[] GetAllItems()
        {
            return _slots.Select(x => x.Item).ToArray();
        }

        public IInventoryItem[] GetEquippedItems()
        {
            return _slots.Select(x => x.Item)
                .Where(x => x.State.IsEquipped == true)
                .ToArray();
        }

        public int GetItemAmount(InventoryType inventoryType)
        {
            return _slots.Count(x => x.Item.InventoryType == inventoryType);
        }

        public bool TryAdd(object sender, IInventoryItem item)
        {
            var notEmptySlotWithSameItem = _slots.FirstOrDefault(x => !x.IsEmpty && x.Item == item);

            if (notEmptySlotWithSameItem != null)
            {
                return TryAddToSlot(sender, notEmptySlotWithSameItem, item);
            }

            var emptySlot = _slots.FirstOrDefault(x => x.IsEmpty);

            if (emptySlot != null)
            {
                return TryAddToSlot(sender, emptySlot, item);
            }
            
            Debug.Log($"Can't add item {item.InventoryType}, Amount: {item.State.Amount}, there is not place for that");

            return false;
        }

        public void Remove(object sender, InventoryType inventoryType, int amount = 1)
        {
            var slotsWithItem = _slots.Where(x => !x.IsEmpty && x.Item.InventoryType == inventoryType).ToArray();

            if (slotsWithItem.Length == 0)
            {
                return;
            }

            foreach (var slot in slotsWithItem)
            {
                slot.Item.State.Amount -= amount;
                if (slot.Item.State.Amount <= 0)
                {
                    slot.Clear();
                    
                    OnInventoryItemRemoved?.Invoke(sender, inventoryType, amount);
                    OnInventoryStateChangedEvent?.Invoke(sender);
                    return;
                }
            }
        }

        public bool HasItem(InventoryType inventoryType, out IInventoryItem inventoryItem)
        {
            inventoryItem = _slots.Where(x => x.Item.InventoryType == inventoryType)
                .Select(x => x.Item)
                .FirstOrDefault();

            return inventoryItem is null;
        }

        public void MoveFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
        {
            if (fromSlot.IsEmpty) return;

            if(toSlot.IsFull) return;
            
            if(!toSlot.IsEmpty && fromSlot.Item.InventoryType != toSlot.Item.InventoryType) return;

            var slotCapacity = fromSlot.Capacity;
            var fits = fromSlot.Amount + toSlot.Amount <= slotCapacity;
            var amountToAdd = fits ? fromSlot.Amount : slotCapacity - toSlot.Amount;
            var amountLeft = fromSlot.Amount - amountToAdd;

            if (toSlot.IsEmpty)
            {
                toSlot.SetItem(toSlot.Item);
                fromSlot.Clear();
                
                OnInventoryStateChangedEvent?.Invoke(sender);
            }

            toSlot.Item.State.Amount += amountToAdd;
            if (fits)
            {
                fromSlot.Clear();
            }
            else
            {
                fromSlot.Item.State.Amount = amountLeft;
            }
            
            OnInventoryStateChangedEvent?.Invoke(sender);
        }
        
        private bool TryAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
        {
            var canAddToNotEmptySlot = slot.Amount + item.State.Amount <= item.Info.MaxItemsInInventorySlot;

            var amountToAdd = canAddToNotEmptySlot ? item.State.Amount : item.Info.MaxItemsInInventorySlot - slot.Amount;
            var amountLeft = item.State.Amount - amountToAdd;

            if (slot.IsEmpty)
            {
                slot.SetItem(item);
            }
            else
            {
                slot.Item.State.Amount += item.State.Amount;
            }
            
            OnInventoryItemAdded?.Invoke(sender, item, amountToAdd);
            OnInventoryStateChangedEvent?.Invoke(sender);

            if (amountLeft <= 0)
            {
                return true;
            }

            item.State.Amount = amountLeft;

            return TryAdd(sender, item);
        }
    }
}