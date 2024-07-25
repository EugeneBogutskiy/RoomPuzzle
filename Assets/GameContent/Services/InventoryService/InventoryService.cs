using GameContent.InventorySystem.SimpleInventorySystem.Abstract;
using GameContent.Services.InventoryService.Abstract;

namespace GameContent.Services.InventoryService
{
    public class InventoryService : IInventoryService
    {
        public IInventorySystem InventorySystem { get; }

        public InventoryService(IInventorySystem inventorySystem)
        {
            InventorySystem = inventorySystem;
        }
    }
}