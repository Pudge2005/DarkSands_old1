using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.ItemsSystem
{
    public abstract class InventoryComponent<TItem, TInventory> : InventoryComponentBase<TItem>
        where TInventory : IInventory<TItem>
    {
        private TInventory _internalInventory;


        public override event InventoryEventArgs<TItem> OnItemCountChanged
        {
            add => _internalInventory.OnItemCountChanged += value;

            remove => _internalInventory.OnItemCountChanged -= value;
        }


        public sealed override int Add(TItem item, int amount) => _internalInventory.Add(item, amount);

        public sealed override bool Contains(TItem item, out int amount) => _internalInventory.Contains(item, out amount);

        public sealed override bool Contains(TItem item, int minAmount) => _internalInventory.Contains(item, minAmount);

        public sealed override TItem Find(Predicate<TItem> predicate) => _internalInventory.Find(predicate);

        public sealed override ReadOnlyMemory<TItem> FindAll(Predicate<TItem> predicate)
            => _internalInventory.FindAll(predicate);

        public sealed override ReadOnlyMemory<TItem> FindAll(Func<TItem, int, bool> predicate)
            => _internalInventory.FindAll(predicate);

        public override IEnumerator<KeyValuePair<TItem, int>> GetEnumerator() => _internalInventory.GetEnumerator();

        public sealed override int Remove(TItem item, int amount) => _internalInventory.Remove(item, amount);

        public sealed override bool TryAdd(TItem item, int amount) => _internalInventory.TryAdd(item, amount);

        public sealed override bool TryFind(Predicate<TItem> predicate, out TItem foundItem)
            => _internalInventory.TryFind(predicate, out foundItem);

        public sealed override bool TryFindAll(Predicate<TItem> predicate, out ReadOnlyMemory<TItem> foundItems)
            => _internalInventory.TryFindAll(predicate, out foundItems);

        public sealed override bool TryRemove(TItem item, int amount)
            => _internalInventory.TryRemove(item, amount);


        protected virtual void Awake()
        {
            _internalInventory = CreateInventory();
        }


        protected abstract TInventory CreateInventory();
    }
}
