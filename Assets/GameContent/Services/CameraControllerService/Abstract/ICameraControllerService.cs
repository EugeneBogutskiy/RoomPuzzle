using UniRx;
using UnityEngine;

namespace GameContent.Services.CameraControllerService.Abstract
{
    public interface ICameraControllerService
    {
        Camera Camera { get; }
        IReadOnlyReactiveProperty<bool> IsOnZoomStage { get; }
        IReactiveCommand<Unit> UnZoomClicked { get; }
    }
}