using System.Collections.Generic;
using DG.Tweening;
using GameContent.PlayerController.Abstract;
using GameContent.Services.CameraControllerService.Abstract;
using GameContent.Services.MouseInput.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace GameContent.PlayerController
{
    public class NavMeshPlayerController : MonoBehaviour, IPlayerController
    {
        private readonly List<Material> _dissolveMaterials = new List<Material>();
        
        private NavMeshAgent _agent;
        private bool _canMove;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            var renderers = GetComponents<Renderer>();
            var childrenRenderers = GetComponentsInChildren<Renderer>();

            foreach (var renderer in renderers)
            {
                _dissolveMaterials.Add(renderer.material);
            }

            foreach (var childrenRenderer in childrenRenderers)
            {
                _dissolveMaterials.Add(childrenRenderer.material);
            }

            MessageBroker.Default.Receive<IMouseInputService>()
                .Subscribe(OnMouseInputServiceReceived)
                .AddTo(this);

            MessageBroker.Default.Receive<ICameraControllerService>()
                .Subscribe(OnCameraControllerServiceReceived)
                .AddTo(this);
        }

        private void OnMouseInputServiceReceived(IMouseInputService mouseInputService)
        {
            mouseInputService.ClickedPosition.Subscribe(OnClickedPositionReceived).AddTo(this);
        }

        private void OnCameraControllerServiceReceived(ICameraControllerService cameraControllerService)
        {
            cameraControllerService.IsOnZoomStage.Subscribe(OnCameraObjectZoomInfoReceived).AddTo(this);
        }

        private void OnClickedPositionReceived(Vector3 destination)
        {
            if(!_canMove) return;
            
            _agent.destination = destination;
        }

        private void OnCameraObjectZoomInfoReceived(bool value)
        {
            _canMove = !_canMove;

            DOTween.Kill("dissolveSequence");
            
            DOTween.Sequence()
                .Append(DOVirtual.Float((value ? 0 : 1), (value ? 1 : 0), 1f, f =>
                    {
                        foreach (var material in _dissolveMaterials)
                        {
                            material.SetFloat("_Amount", f);
                        }
                    }))
                .SetId("dissolveSequence");
            
            Debug.Log($"{nameof(NavMeshPlayerController)}: Player received zoom object info");
        }
    }
}