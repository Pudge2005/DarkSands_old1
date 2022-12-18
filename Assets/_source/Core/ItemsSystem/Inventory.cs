using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.ItemsSystem
{
    public delegate void InventoryEventArgs<TItem>(TItem item, int countDelta, int newCount);

    public interface IInventory<TItem> : IEnumerable<KeyValuePair<TItem, int>>
    {
        event System.Action<InventoryEventArgs<TItem>> OnItemCountChanged;

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

        bool TryFind(Predicate<TItem> predicate, out TItem foundItem);
        bool TryFindAll(Predicate<TItem> predicate, out ReadOnlyMemory<TItem> foundItems);
    }


    //public abstract class Inventory<TItem> : IInventory<TItem>
    //{
    //    private readonly DevourDev.
    //}
}
