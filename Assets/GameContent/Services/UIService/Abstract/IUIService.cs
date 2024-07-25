using Configs;
using UniRx;
using UnityEngine;

namespace GameContent.Services.UIService.Abstract
{
    public interface IUIService
    {
        IReactiveCommand<Unit> SaveCommand { get; }
        IReactiveCommand<Unit> LoadCommand { get; }
        IReactiveCommand<Unit> ExitCommand { get; }

        GameObject OpenView(UIViewNames.VewNames vew);
    }
}