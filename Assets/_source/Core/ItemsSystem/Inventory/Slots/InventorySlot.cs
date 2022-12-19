using Codice.CM.Common;
using UnityEditor;
using UnityEditor.Graphs;

namespace Game.Core.ItemsSystem
{
    public sealed class InventorySlot : IInventorySlot
    {
        private readonly int _id;
        private ItemData _itemData;
        private int _amount;


        public InventorySlot(int slotID)
        {
            _id = slotID;
        }


        public int SlotID => _id;
        public ItemData Item => _itemData;
        public int Amount => _amount;

        public bool Empty => _itemData == null;


        public event System.Action<IInventorySlot, ReadOnlyItemAmount, ReadOnlyItemAmount> OnChanged;


        internal void Set(ItemData itemData, int amount)
        {
            var tmp = new ReadOnlyItemAmount(this);
            _itemData = itemData;
            _amount = amount;
            var cur = new ReadOnlyItemAmount(this);
            OnChanged?.Invoke(this, tmp, cur);
        }
    }
}
