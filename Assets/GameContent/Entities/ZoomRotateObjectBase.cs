using System;
using DG.Tweening;
using GameContent.Entities.Abstract;
using GameContent.Services.CameraControllerService.Abstract;
using UniRx;
using UnityEngine;

namespace GameContent.Entities
{
    [RequireComponent(typeof(Collider))]
    public abstract class ZoomRotateObjectBase : MonoBehaviour, IZoomRotate
    {
        [SerializeField] private string _id;
        [SerializeField] private float _objectFocusPoint = 3f;
        [SerializeField] private float _objectScaleFactor = 3f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private AnimationConfig _animationConfig;

        protected ICameraControllerService _cameraControllerService;
        protected Rigidbody _rigidbody;
        protected Collider _collider;
        protected bool _canRotate;
        protected bool _isObjectOnZoomStage;

        private Transform _cameraTransform;
        private Vector3 _originalPosition;
        private Vector3 _originalScale;
        private Quaternion _originalRotation;

        private Sequence animationSequence;

        public string Id => _id;

        public virtual void Select(ICameraControllerService cameraControllerService)
        {
            if (_cameraControllerService == null)
            {
                _cameraControllerService = cameraControllerService;

                _cameraControllerService.UnZoomClicked.Subscribe(_ => Deselect()).AddTo(this);
            }
            
            if (cameraControllerService.IsOnZoomStage.Value && !_isObjectOnZoomStage)
            {
                _cameraTransform = cameraControllerService.Camera.transform;

                Vector3 focusObjectPosition = _cameraTransform.position + _cameraTransform.forward * _objectFocusPoint;
                
                Vector3 newScale = new Vector3(transform.localScale.x * _objectScaleFactor,
                    transform.localScale.y * _objectScaleFactor, transform.localScale.z * _objectScaleFactor);
                
                animationSequence?.Kill();

                animationSequence.Append(transform.DOMove(focusObjectPosition, _animationConfig.moveInSpeed))
                    .SetEase(_animationConfig.moveInEase);
                animationSequence.Join(transform.DOScale(newScale, _animationConfig.moveInSpeed))
                    .SetEase(_animationConfig.moveInScaleEase);
                animationSequence.Join(transform.DOLookAt(_cameraTransform.position, _animationConfig.moveInSpeed));

                _canRotate = true;
                _isObjectOnZoomStage = true;
            }
        }

        public virtual void Deselect()
        {
            animationSequence?.Kill();
            
            animationSequence.Append(transform.DOMove(_originalPosition, _animationConfig.moveOutSpeed))
                .SetEase(_animationConfig.moveOutEase);
            animationSequence.Join(transform.DOScale(_originalScale, _animationConfig.moveOutSpeed))
                .SetEase(_animationConfig.moveOutScaleEase);
            animationSequence.Join(transform.DORotateQuaternion(_originalRotation, _animationConfig.moveOutSpeed));

            _canRotate = false;
            _isObjectOnZoomStage = false;
        }

        private void OnEnable()
        {
            _originalPosition = transform.position;
            _originalScale = transform.localScale;
            _originalRotation = transform.rotation;
        }

        private void Update()
        {
            if (_canRotate)
            {
                transform.Rotate((Input.GetAxis("Mouse X") * _rotationSpeed),
                    (Input.GetAxis("Mouse Y") * _rotationSpeed), 0, Space.World);
            }
        }
        
        [Serializable]
        public class AnimationConfig
        {
            public float moveInSpeed = 1f;
            public Ease moveInEase;
            public Ease moveInScaleEase;
            public float moveOutSpeed = 0.5f;
            public Ease moveOutEase;
            public Ease moveOutScaleEase;
        }
    }
}
