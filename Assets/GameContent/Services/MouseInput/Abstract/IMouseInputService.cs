using UniRx;
using UnityEngine;

namespace GameContent.Services.MouseInput.Abstract
{
    public interface IMouseInputService
    {
        public ReactiveCommand<GameObject> ClickedObject { get; }
        public ReactiveCommand<Vector3> ClickedPosition { get; }
        
        public ReactiveCommand<Unit> RotateLeft { get; }
        public ReactiveCommand<Unit> RotateRight { get; }
        public ReactiveCommand<float> ZoomCommand { get; }
    }
}