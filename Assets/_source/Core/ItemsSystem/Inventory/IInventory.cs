using System;
using System.Collections.Generic;

namespace Game.Core.ItemsSystem
{
    public interface IInventory : IEnumerable<IInventorySlot>
    {
        int Capacity { get; }

        event Action<Inventory, IItemAmount> OnItemsAdded;
        event Action<Inventory, IItemAmount> OnItemsRemoved;

        int Add(IItemAmount itemAmount);
        int Add(ItemData item, int amount);
        ReadOnlyMemory<InventorySlot> FindAll(Inventory.SlotPredicate slotPredicate);
        ReadOnlyMemory<InventorySlot> FindAllSlotsWithItemData(ItemData itemData);
        int Remove(IItemAmount itemAmount);
        int Remove(ItemData item, int amount);
        bool TryAdd(IItemAmount itemAmount, out int canAddAmount);
        bool TryFind(Inventory.ItemDataPredicate itemDataPredicate, out InventorySlot slot);
        bool TryFind(Inventory.ItemReferencePredicate itemReferencePredicate, out InventorySlot slot);
        bool TryFind(Inventory.SlotPredicate slotPredicate, out InventorySlot slot);
        bool TryRemove(IItemAmount itemAmount, out int canRemoveAmount);
    }
}