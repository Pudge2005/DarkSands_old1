namespace Game.Core.ItemsSystem
{
    public interface IItemAmount
    {
        ItemData Item { get; }
        int Amount { get; }
    }
}
