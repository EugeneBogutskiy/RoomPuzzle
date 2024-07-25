using UniRx;
using UnityEngine;

namespace GameContent.Settings.MouseInputSettings
{
    [CreateAssetMenu(menuName = "Evgenoid/MouseInputSettings", fileName = "MouseInputSettings")]
    public class MouseInputSettings : ScriptableObject
    {
        [Header("Common settings")]
        public BoolReactiveProperty canMoveScene;
        public BoolReactiveProperty canMoveCharacter;

        [Tooltip("minimum mouse delta to rotate camera")] [Space(20)]
        public float minimumMouseDelta;

        [Header("Mouse zoom settings")] [Space(20)]
        public BoolReactiveProperty canZoom;
        public float zoomAmount;

        private void OnEnable()
        {
            canMoveScene.Value = true;
            canMoveCharacter.Value = true;
        }
    }
}