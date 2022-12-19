namespace Game.Core.ItemsSystem
{
    public interface IInventorySlot : IItemAmount
    {
        int SlotID { get; }
        bool Empty { get; }


        event System.Action<IInventorySlot, ReadOnlyItemAmount, ReadOnlyItemAmount> OnChanged;
    }
}
