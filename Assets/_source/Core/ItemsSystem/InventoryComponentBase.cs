using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.ItemsSystem
{
    public abstract class InventoryComponentBase<TItem> : MonoBehaviour, IInventory<TItem>
    {
        public abstract event InventoryEventArgs<TItem> OnItemCountChanged;


        public abstract int Add(TItem item, int amount);
        public abstract bool Contains(TItem item, out int amount);
        public abstract bool Contains(TItem item, int minAmount);
        public abstract TItem Find(Predicate<TItem> predicate);
        public abstract ReadOnlyMemory<TItem> FindAll(Predicate<TItem> predicate);
        public abstract ReadOnlyMemory<TItem> FindAll(Func<TItem, int, bool> predicate);
        public abstract IEnumerator<KeyValuePair<TItem, int>> GetEnumerator();
        public abstract int Remove(TItem item, int amount);
        public abstract bool TryAdd(TItem item, int amount);
        public abstract bool TryFind(Predicate<TItem> predicate, out TItem foundItem);
        public abstract bool TryFindAll(Predicate<TItem> predicate, out ReadOnlyMemory<TItem> foundItems);
        public abstract bool TryRemove(TItem item, int amount);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
