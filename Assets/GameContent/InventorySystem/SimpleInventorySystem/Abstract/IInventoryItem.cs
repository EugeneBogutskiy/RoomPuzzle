namespace GameContent.InventorySystem.SimpleInventorySystem.Abstract
{
    public interface IInventoryItem
    {
        void AddToStack(int count);
        void RemoveFromStack(int count);
    }
}