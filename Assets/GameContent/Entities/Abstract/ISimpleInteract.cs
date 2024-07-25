using GameContent.Services.CameraControllerService.Abstract;

namespace GameContent.Entities
{
    public interface ISimpleInteract
    {
        public void Interact(ICameraControllerService cameraControllerService);
    }
}
