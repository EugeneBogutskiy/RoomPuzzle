using DG.Tweening;
using GameContent.Services.SceneService.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _circleSprite;
    [SerializeField] private float _delayBeforeCloseScreen = 0.2f;

    public void Init(ISceneService sceneService)
    {
        sceneService.LoadingPercentage
            .Subscribe(x => _circleSprite.fillAmount = x)
            .AddTo(this);

        sceneService.LoadingPercentage
            .Subscribe(x =>
            {
                if (x == 1)
                {
                    CloseScreen();
                }
            }).AddTo(this);
    }

    private void CloseScreen()
    {
        DOVirtual.DelayedCall(_delayBeforeCloseScreen, () => Destroy(this.gameObject));
    }
}
