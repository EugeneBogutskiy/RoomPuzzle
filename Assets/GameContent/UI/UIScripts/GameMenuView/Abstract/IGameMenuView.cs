using UniRx;

namespace GameContent.UI.UIScripts.GameMenuView.Abstract
{
    public interface IGameMenuView
    {
        IReactiveCommand<Unit> Save { get; }
        IReactiveCommand<Unit> Load { get; }
        IReactiveCommand<Unit> Exit { get; }
    }
}