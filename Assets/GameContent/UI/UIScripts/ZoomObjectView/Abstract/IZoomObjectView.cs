using UniRx;

namespace GameContent.UI.UIScripts.ZoomObjectView.Abstract
{
    public interface IZoomObjectView
    {
        IReactiveCommand<Unit> BackCommand { get; }
    }
}