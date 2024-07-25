using DG.Tweening;
using GameContent.Entities;
using GameContent.Services.CameraControllerService.Abstract;
using UnityEngine;

public class BlueCupInteractableObject : InteractWithStateObjectBase
{
    [SerializeField] private float _maxRotationAngle = 90f;
    [SerializeField] private float _rotationTime = 0.5f;
    [SerializeField] private Ease _rotationEase;
    
    private Sequence animationSequence;
    
    public override void Interact(ICameraControllerService cameraControllerService)
    {
        base.Interact(cameraControllerService);

        if (cameraControllerService.IsOnZoomStage.Value)
        {
            animationSequence?.Kill();

            float randomAngle = Random.Range(0, _maxRotationAngle);

            Vector3 newRotationAngle = new Vector3(transform.localEulerAngles.x,
                transform.localEulerAngles.y, transform.localEulerAngles.z + randomAngle);

            animationSequence.Append(transform.DORotate(newRotationAngle, _rotationTime))
                .SetEase(_rotationEase);
        }
    }
}
