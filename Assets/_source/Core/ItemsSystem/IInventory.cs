using System;
using System.Collections.Generic;

namespace Game.Core.ItemsSystem
{
    public interface IInventory<TItem> : IEnumerable<KeyValuePair<TItem, int>>
    {
        event InventoryEventArgs<TItem> OnItemCountChanged;

        bool TryAdd(TItem item, int amount);
        bool Contains(TItem item, out int amount);
        bool Contains(TItem item, int minAmount);

        bool TryRemove(TItem item, int amount);

        /// <returns>added items amount</returns>
        int Add(TItem item, int amount);

        /// <returns>removed items amount</returns>
        int Remove(TItem item, int amount);

        TItem Find(Predicate<TItem> predicate);

        ReadOnlyMemory<TItem> FindAll(Predicate<TItem> predicate);
        ReadOnlyMemory<TItem> FindAll(System.Func<TItem, int, bool> predicate);

        bool TryFind(Predicate<TItem> predicate, out TItem foundItem);

        bool TryFindAll(Predicate<TItem> predicate, out ReadOnlyMemory<TItem> foundItems);
    }
}
