using UniRx;

namespace GameContent.Services.SceneService.Abstract
{
    public interface ISceneService
    {
        IReadOnlyReactiveProperty<float> LoadingPercentage { get; }

        void SaveScene();
        void LoadFromSaveData();
        void LoadScene(string sceneId);
    }
}