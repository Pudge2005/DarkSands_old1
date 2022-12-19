namespace Game.Core.ItemsSystem
{
    public readonly struct ReadOnlyItemAmount : IItemAmount
    {
        private readonly ItemData _item;
        private readonly int _amount;


        public ReadOnlyItemAmount(ItemData item, int amount)
        {
            _item = item;
            _amount = amount;
        }      

        public ReadOnlyItemAmount(IItemAmount itemAmount)
        {
            _item = itemAmount.Item;
            _amount = itemAmount.Amount;
        }


        public ItemData Item => _item;
        public int Amount => _amount;
    }
}
