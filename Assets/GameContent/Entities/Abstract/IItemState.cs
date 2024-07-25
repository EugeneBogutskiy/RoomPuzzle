namespace GameContent.Entities.Abstract
{
    public interface IItemState
    {
        void SetState(InteractableData data);
        InteractableData GetState();
        void RestoreDefaultState();
    }
}