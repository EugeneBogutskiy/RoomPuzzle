using GameContent.Services.MouseInput.Abstract;
using GameContent.Settings.MouseInputSettings;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameContent.Services.MouseInput
{
    public class MouseInputService : IMouseInputService
    {
        private readonly MouseInputSettings _mouseInputSettings;

        private readonly ReactiveCommand<GameObject> _clickedObject;
        private readonly ReactiveCommand<Vector3> _clickedPosition;
        private readonly ReactiveCommand<Unit> _rotateLeft;
        private readonly ReactiveCommand<Unit> _rotateRight;
        private readonly ReactiveCommand<float> _zoomCommand;

        private Vector3 _startMousePosition;
        private Vector3 _endMousePosition;

        public ReactiveCommand<GameObject> ClickedObject => _clickedObject;
        public ReactiveCommand<Vector3> ClickedPosition => _clickedPosition;
        public ReactiveCommand<Unit> RotateLeft => _rotateLeft;
        public ReactiveCommand<Unit> RotateRight => _rotateRight;
        public ReactiveCommand<float> ZoomCommand => _zoomCommand;

        public MouseInputService(MouseInputSettings settings)
        {
            _mouseInputSettings = settings;

            _clickedObject = new ReactiveCommand<GameObject>();
            _clickedPosition = new ReactiveCommand<Vector3>();
            _rotateLeft = new ReactiveCommand<Unit>();
            _rotateRight = new ReactiveCommand<Unit>();
            _zoomCommand = new ReactiveCommand<float>();

            Init();
        }

        private void Init()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => OnMouseButtonDownClicked());

            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonUp(0))
                .Subscribe(_ => OnMouseButtonUp());

            Observable.EveryUpdate()
                .Select(x => Input.GetAxis("Mouse ScrollWheel"))
                .Where(x => x != 0)
                .Subscribe(OnZoom);
        }

        private void OnMouseButtonDownClicked()
        {
            _startMousePosition = Input.mousePosition;
        }
        
        private void OnMouseButtonUp()
        {
            _endMousePosition = Input.mousePosition;
            
            var isMouseOverUI = EventSystem.current.IsPointerOverGameObject();

            if (isMouseOverUI) return;

            if (Vector3.Distance(_endMousePosition, _startMousePosition) < _mouseInputSettings.minimumMouseDelta)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
                {
                    _clickedObject.Execute(hit.transform.gameObject);

                    if (_mouseInputSettings.canMoveCharacter.Value)
                    {
                        _clickedPosition.Execute(hit.point);
                    }
                }
            }

            if (!_mouseInputSettings.canMoveScene.Value) return;
            
            if (_endMousePosition.x - _startMousePosition.x > _mouseInputSettings.minimumMouseDelta)
            {
                _rotateRight.Execute(Unit.Default);
            }
            if (_endMousePosition.x - _startMousePosition.x < - _mouseInputSettings.minimumMouseDelta)
            {
                _rotateLeft.Execute(Unit.Default);
            }
        }

        private void OnZoom(float value)
        {
            if(!_mouseInputSettings.canZoom.Value) return;
            
            if (value < 0)
                _zoomCommand.Execute(_mouseInputSettings.zoomAmount);
            else
                _zoomCommand.Execute(-_mouseInputSettings.zoomAmount);
        }
    }
}