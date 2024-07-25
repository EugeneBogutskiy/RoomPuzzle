using DG.Tweening;
using UniRx;
using UnityEngine;

namespace GameContent.Settings.CameraSettings
{
    [CreateAssetMenu(menuName = "Evgenoid/CameraSettings", fileName = "CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        public BoolReactiveProperty canMove;
        public BoolReactiveProperty canZoom;
        public float minZoom;
        public float maxZoom;
        public float rotationTime;
        public float zoomTime;
        public int rotationAngle;

        [Header("Rotation type")]
        public Ease easeType;

        [Header("Zoom on objects settings")] 
        public float minObjectZoom;
        public float maxObjectZoom;
        public float objectZoomTime;

        [Header("Zoom on object type")]
        public Ease zoomOnObjectEaseType;

        private void OnEnable()
        {
            canMove.Value = true;
            canZoom.Value = true;
        }
    }
}