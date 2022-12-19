namespace Game.Core.ItemsSystem
{
    public class ItemData
    {
        private readonly ItemSo _reference;

        //state (durability, uses left, buffs/enchantments etc...)


        public ItemData(ItemSo reference)
        {
            _reference = reference;
        }


        public ItemSo Reference => _reference;
    }
}
