using GameContent.Services.CameraControllerService.Abstract;

namespace GameContent.Entities.Abstract
{
    public interface IZoomRotate
    {
        void Select(ICameraControllerService cameraControllerService);
        void Deselect();
    }
}
