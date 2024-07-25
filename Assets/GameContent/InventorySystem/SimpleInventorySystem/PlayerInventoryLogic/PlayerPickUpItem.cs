using GameContent.InventorySystem.SimpleInventorySystem.Abstract;
using GameContent.Services.InventoryService.Abstract;
using UniRx;
using UnityEngine;

namespace GameContent.InventorySystem.SimpleInventorySystem.PlayerInventoryLogic
{
    public class PlayerPickUpItem : MonoBehaviour
    {
        private IInventoryService _inventoryService;

        private void Awake()
        {
            MessageBroker.Default.Receive<IInventoryService>()
                .Subscribe(x => _inventoryService = x)
                .AddTo(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IItemObject>(out var itemObject))
            {
                _inventoryService.InventorySystem.Add(itemObject.ReferenceItem);
                itemObject.PickUpItem();
            }
        }
    }
}