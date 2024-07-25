using DG.Tweening;
using UnityEngine;

namespace GameContent.Settings.WallServiceSettings
{
    [CreateAssetMenu(menuName = "Evgenoid/WallServiceSettings", fileName = "WallServiceSettings")]
    public class WallServiceSettings : ScriptableObject
    {
        public float wallOffsetY;
        public float moveTime;
        
        public MoveWallDirection wallDisappearDirection;

        public Ease easeType;
    }

    public enum MoveWallDirection
    {
        Up,
        Back
    }
}