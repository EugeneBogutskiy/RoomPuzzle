using GameContent.InventorySystem.SimpleInventorySystem.Abstract;

namespace GameContent.Services.InventoryService.Abstract
{
    public interface IInventoryService
    {
        IInventorySystem InventorySystem { get; }
    }
}