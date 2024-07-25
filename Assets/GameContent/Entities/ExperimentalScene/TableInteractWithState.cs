using GameContent.Services.CameraControllerService.Abstract;
using UnityEngine;

namespace GameContent.Entities.ExperimentalScene
{
    public class TableInteractWithState : InteractWithStateObjectBase
    {
        public override void Interact(ICameraControllerService cameraControllerService)
        {
            base.Interact(cameraControllerService);

            if (!cameraControllerService.IsOnZoomStage.Value)
            {
                transform.Rotate(Vector3.up, 15);
            }
        }
    }
}