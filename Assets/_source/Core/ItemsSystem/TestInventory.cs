using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevourDev.Base.Collections.Generic;

namespace Game.Core.ItemsSystem
{

    public delegate void InventoryEventArgs<TItem>(IInventory<TItem> inventory, TItem item, int countDelta, int newCount);


    public class TestInventory<TItem> : IInventory<TItem>
    {
        private readonly CountingDictionary<TItem> _items = new(false, 0);


        public event InventoryEventArgs<TItem> OnItemCountChanged;


        private void RaiseEvent(TItem item, int delta, int newCount)
        {
            OnItemCountChanged?.Invoke(this, item, delta, newCount);
        }

        public int Add(TItem item, int amount)
        {
            var newAmount = _items.AddAmount(item, amount);
            RaiseEvent(item, amount, newAmount);
            return amount;
        }

        public bool Contains(TItem item, out int amount)
        {
            return _items.TryGetAmount(item, out amount);
        }

        public bool Contains(TItem item, int minAmount)
        {
            if (!Contains(item, out var amount))
                return false;

            return amount >= minAmount;
        }

        public TItem Find(Predicate<TItem> predicate)
        {
            _ = _items.TryFindItem(predicate, out var result);
            return result;
        }

        public ReadOnlyMemory<TItem> FindAll(Predicate<TItem> predicate)
        {
            return _items.FindAll(predicate);
        }

        public ReadOnlyMemory<TItem> FindAll(System.Func<TItem, int, bool> predicate)
        {
            return _items.FindAll(predicate);
        }

        public IEnumerator<KeyValuePair<TItem, int>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public int Remove(TItem item, int amount)
        {
            int removed = _items.RemoveAmountOrAny(item, amount);
            RaiseEvent(item, -removed, _items.GetAmount(item));
            return removed;
        }

        public bool TryAdd(TItem item, int amount)
        {
            _ = _items.AddAmount(item, amount);
            return true;
        }

        public bool TryFind(Predicate<TItem> predicate, out TItem foundItem)
        {
            foundItem = Find(predicate);
            return foundItem != null;
        }

        public bool TryFindAll(Predicate<TItem> predicate, out ReadOnlyMemory<TItem> foundItems)
        {
            foundItems = FindAll(predicate);
            return foundItems.Length > 0;
        }

        public bool TryRemove(TItem item, int amount)
        {
            return _items.TryRemoveExactAmount(item, amount);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
