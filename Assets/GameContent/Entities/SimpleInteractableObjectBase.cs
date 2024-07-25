using GameContent.Services.CameraControllerService.Abstract;
using UnityEngine;

namespace GameContent.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class SimpleInteractableObjectBase : MonoBehaviour, ISimpleInteract
    {
        [SerializeField] private string _id;

        protected ICameraControllerService _cameraControllerService;
        protected Rigidbody _rigidbody;
        protected Collider _collider;
        
        public string Id => _id;

        public virtual void Interact(ICameraControllerService cameraControllerService)
        {
            if (_cameraControllerService == null)
            {
                _cameraControllerService = cameraControllerService;
            }
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }
    }
}
