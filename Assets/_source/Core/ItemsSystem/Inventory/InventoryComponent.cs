using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.ItemsSystem
{
    public sealed class InventoryComponent : CharacterComponent, IInventory
    {
        [SerializeField] private int _inventorySize = 16;
        private Inventory _internalInventory;


        protected override void InitCharacterComponent()
        {
            base.InitCharacterComponent();
            _internalInventory = new(_inventorySize);
        }

        #region IInventory realisation
        public int Capacity => _internalInventory.Capacity;

        public event Action<Inventory, IItemAmount> OnItemsAdded
        {
            add
            {
                _internalInventory.OnItemsAdded += value;
            }

            remove
            {
                _internalInventory.OnItemsAdded -= value;
            }
        }

        public event Action<Inventory, IItemAmount> OnItemsRemoved
        {
            add
            {
                _internalInventory.OnItemsRemoved += value;
            }

            remove
            {
                _internalInventory.OnItemsRemoved -= value;
            }
        }

        public int Add(IItemAmount itemAmount)
        {
            return _internalInventory.Add(itemAmount);
        }

        public int Add(ItemData item, int amount)
        {
            return _internalInventory.Add(item, amount);
        }

        public ReadOnlyMemory<InventorySlot> FindAll(Inventory.SlotPredicate slotPredicate)
        {
            return _internalInventory.FindAll(slotPredicate);
        }

        public ReadOnlyMemory<InventorySlot> FindAllSlotsWithItemData(ItemData itemData)
        {
            return _internalInventory.FindAllSlotsWithItemData(itemData);
        }

        public IEnumerator<IInventorySlot> GetEnumerator()
        {
            return _internalInventory.GetEnumerator();
        }

        public int Remove(IItemAmount itemAmount)
        {
            return _internalInventory.Remove(itemAmount);
        }

        public int Remove(ItemData item, int amount)
        {
            return _internalInventory.Remove(item, amount);
        }

        public bool TryAdd(IItemAmount itemAmount, out int canAddAmount)
        {
            return _internalInventory.TryAdd(itemAmount, out canAddAmount);
        }

        public bool TryFind(Inventory.ItemDataPredicate itemDataPredicate, out InventorySlot slot)
        {
            return _internalInventory.TryFind(itemDataPredicate, out slot);
        }

        public bool TryFind(Inventory.ItemReferencePredicate itemReferencePredicate, out InventorySlot slot)
        {
            return _internalInventory.TryFind(itemReferencePredicate, out slot);
        }

        public bool TryFind(Inventory.SlotPredicate slotPredicate, out InventorySlot slot)
        {
            return _internalInventory.TryFind(slotPredicate, out slot);
        }

        public bool TryRemove(IItemAmount itemAmount, out int canRemoveAmount)
        {
            return _internalInventory.TryRemove(itemAmount, out canRemoveAmount);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_internalInventory).GetEnumerator();
        }
        #endregion


        public void SwapSlots(int indexA, int indexB)
        {
            var slotA = _internalInventory[indexA];
            var slotB = _internalInventory[indexB];

            var tmp = new ReadOnlyItemAmount(slotA);

            slotA.Set(slotB.Item, slotB.Amount);
            slotB.Set(tmp.Item, tmp.Amount);
        }
    }
}
