namespace Game.Core.ItemsSystem
{
    public class TestInventoryComponent : InventoryComponent<ItemSo, TestInventory<ItemSo>>
    {
        protected override TestInventory<ItemSo> CreateInventory()
        {
            return new();
        }
    }
}
