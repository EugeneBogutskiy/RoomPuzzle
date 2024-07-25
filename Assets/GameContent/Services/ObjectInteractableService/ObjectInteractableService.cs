using GameContent.Entities;
using GameContent.Entities.Abstract;
using GameContent.Services.CameraControllerService.Abstract;
using GameContent.Services.MouseInput.Abstract;
using GameContent.Services.ObjectInteractableService.Abstract;
using UniRx;
using UnityEngine;

namespace GameContent.Services.ObjectInteractableService
{
    public class ObjectInteractableService : IObjectInteractableService
    {
        private ICameraControllerService _cameraControllerService;
        public ObjectInteractableService()
        {
            MessageBroker.Default.Receive<IMouseInputService>().Subscribe(OnServiceReceived);
            MessageBroker.Default.Receive<ICameraControllerService>().Subscribe(x => _cameraControllerService = x);
        }

        private void OnServiceReceived(IMouseInputService mouseInputService)
        {
            mouseInputService.ClickedObject.Subscribe(OnObjectClicked);
        }

        private void OnObjectClicked(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<IInteractWithState>(out var interactComponent))
            {
                interactComponent.Interact(_cameraControllerService);
            }

            if (gameObject.TryGetComponent<ISimpleInteract>(out var simpleInteractComponent))
            {
                simpleInteractComponent.Interact(_cameraControllerService);
            }

            if (gameObject.TryGetComponent<IZoomRotate>(out var zoomRotateComponent))
            {
                zoomRotateComponent.Select(_cameraControllerService);
            }
        }
    }
}