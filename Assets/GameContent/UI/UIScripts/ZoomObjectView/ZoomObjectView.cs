using GameContent.UI.UIScripts.ZoomObjectView.Abstract;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent.UI.UIScripts.ZoomObjectView
{
    public class ZoomObjectView : MonoBehaviour, IZoomObjectView
    {
        private readonly ReactiveCommand<Unit> _backCommand = new ReactiveCommand<Unit>();

        [SerializeField]
        private Button _backButton;

        public IReactiveCommand<Unit> BackCommand => _backCommand;

        void OnEnable()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => _backCommand.Execute(Unit.Default)).AddTo(this);
        }
    }
}