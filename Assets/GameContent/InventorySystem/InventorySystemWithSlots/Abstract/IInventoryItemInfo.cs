using UnityEngine;

namespace GameContent.InventorySystem.InventorySystemWithSlots.Abstract
{
    public interface IInventoryItemInfo
    {
        string Id { get; }
        string Title { get; }
        string Description { get; }
        int MaxItemsInInventorySlot { get; }
        Sprite SpriteIcon { get; }
    }
}