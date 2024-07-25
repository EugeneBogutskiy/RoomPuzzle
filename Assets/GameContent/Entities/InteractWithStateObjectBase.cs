using GameContent.Entities.Abstract;
using GameContent.Services.CameraControllerService.Abstract;
using UnityEngine;

namespace GameContent.Entities
{
    [RequireComponent(typeof(Collider))]
    public abstract class InteractWithStateObjectBase : MonoBehaviour, IInteractWithState, IItemState
    {
        [SerializeField] private string _id;
        
        private InteractableData _initialData;

        protected ICameraControllerService _cameraControllerService;
        protected Rigidbody _rigidbody;
        protected Collider _collider;

        public string Id => _id;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _initialData = new InteractableData(transform, _id);
        }

        public virtual void Interact(ICameraControllerService cameraControllerService)
        {
            if (_cameraControllerService == null)
            {
                _cameraControllerService = cameraControllerService;
            }
        }

        public void SetState(InteractableData data)
        {
            transform.position = data.Position;
            transform.rotation = data.Rotation;
        }

        public InteractableData GetState()
        {
            return new InteractableData(transform, _id);
        }

        public void RestoreDefaultState()
        {
            _initialData.RestoreState(transform);
        }
    }

    public struct InteractableData
    {
        public string Id { get; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public InteractableData(Transform transform, string id)
        {
            Id = id;
            Position = transform.position;
            Rotation = transform.rotation;
        }

        public void RestoreState(Transform transform)
        {
            transform.position = Position;
            transform.rotation = Rotation;
        }
    }
}