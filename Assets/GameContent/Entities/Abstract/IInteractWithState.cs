using GameContent.Services.CameraControllerService.Abstract;

namespace GameContent.Entities.Abstract
{
    public interface IInteractWithState
    {
        void Interact(ICameraControllerService cameraControllerService);
    }
}