using System;
using Codice.CM.Common;
using UnityEditor;
using UnityEditor.Graphs;

namespace Game.Core.ItemsSystem
{
    public sealed class InventorySlot : IItemAmount
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


        internal void Set(ItemData itemData, int amount)
        {
            _itemData = itemData;
            _amount = amount;
        }
    }

    public interface IItemAmount
    {
        ItemData Item { get; }
        int Amount { get; }
    }

    public sealed class Inventory
    {
        //TODO: dynamicallyExpandable Buffers
        //TODO: instructions pool

        private class Instruction : IDisposable
        {
            private InventorySlot _slot;
            private ItemData _item;
            private int _amount;


            public Instruction()
            {

            }


            public void Set(InventorySlot slot, ItemData item, int amount)
            {
                _slot = slot;
                _item = item;
                _amount = amount;
            }


            public void Execute()
            {
                _slot.Set(_item, _amount);
                Dispose();
            }

            public void Dispose()
            {
                _slot = null;
                _item = null;
            }
        }


        public delegate bool ItemReferencePredicate(ItemSo itemReference);
        public delegate bool ItemDataPredicate(ItemData itemData);
        public delegate bool SlotPredicate(InventorySlot slot);


        private readonly InventorySlot[] _slots;
        private readonly int _capacity;


        private static InventorySlot[] _slotsBuffer;
        private static ReadOnlyMemory<InventorySlot> _slotsMem;

        private static Instruction[] _precalculatedInstructionsBuffer;
        private static ReadOnlyMemory<Instruction> _precalculatedInstructionsMem;


        public Inventory(int slotsCount)
        {
            _slots = new InventorySlot[slotsCount];
            _capacity = slotsCount;

            if (_slotsBuffer == null || _slotsBuffer.Length < slotsCount) //toremove: implement dynamic expanding
            {
                _slotsBuffer = new InventorySlot[slotsCount];
                _slotsMem = new(_slotsBuffer);

                _precalculatedInstructionsBuffer = new Instruction[slotsCount];
                _precalculatedInstructionsMem = new(_precalculatedInstructionsBuffer);

                _precalculatedInstructionsBuffer.Initialize(); //toremove: use pool
            }

            InitSlots(0, _capacity);
        }


        public InventorySlot this[int slotIndex] => _slots[slotIndex];
        public int Capacity => _capacity;


        public bool TryAdd(IItemAmount itemAmount, out int canAddAmount) => AddInternal(itemAmount.Item, itemAmount.Amount, out canAddAmount, false);

        public bool TryRemove(IItemAmount itemAmount, out int canRemoveAmount) => TryRemove(itemAmount.Item, itemAmount.Amount, out canRemoveAmount, false);

        private bool AddInternal(ItemData itemData, int amount, out int canAddAmount, bool addAllPossible)
        {
            //если предмет стакается - сначала распихиваем его
            //по слотам, где он уже есть, но меньше лимита стака;
            //если такие слоты закончились - используем пустые слоты
            //если места хватает - добавляем

            int instructionsCount = 0;
            int amountLeft = amount;

            if (itemData.Reference.StackSize > 1)
            {
                EmulateAddingStackable(itemData, ref amountLeft, ref instructionsCount);
            }
            else
            {
                EmulateAddingToEmptySlots(itemData, ref amountLeft, ref instructionsCount);
            }

            canAddAmount = amount - amountLeft;
            bool canAddAll = amountLeft == 0;

            if (!canAddAll)
            {
                //optionally
                //DisposeUnusedInstructions(instructionsCount);

                return false;
            }

            ExecuteInstructions(instructionsCount);
            return true;
        }

        /// <param name="removeAllPossible">remove all event if existing items count < amount </param>
        public bool TryRemove(ItemData itemData, int amount, out int canRemoveAmount, bool removeAllPossible)
        {
#if UNITY_EDITOR
            if (amount < 0)
                throw new ArgumentException($"attempt to remove negative amount: {itemData}, {amount}");
#endif
            var slots = FindAllSlotsWithItemData(itemData).Span;
            int instructionsCount = 0;
            int left = amount;

            foreach (var slot in slots)
            {
                int rem = slot.Amount;

                if (rem > left)
                    rem = left;

                int remainsInSlot = slot.Amount - rem;
                AddInstruction(ref instructionsCount, slot, remainsInSlot > 0 ? itemData : null, remainsInSlot);
                left -= rem;

                if (left == 0)
                    break;
            }

            bool canRemoveAll = left == 0;

            if (canRemoveAll || removeAllPossible)
            {
                ExecuteInstructions(instructionsCount);
            }
            else
            {
                //optionally
                //DisposeUnusedInstructions(instructionsCount);
            }

            canRemoveAmount = amount - left;
            return canRemoveAll;
        }



        public bool TryFind(ItemReferencePredicate itemReferencePredicate, out InventorySlot slot)
        {
            var span = _slots.AsSpan();
            var length = _capacity;

            for (int i = 0; i < length; i++)
            {
                slot = span[i];

                if (slot == null)
                    continue;

                if (itemReferencePredicate(slot.Item.Reference))
                    return true;
            }

            slot = null;
            return false;
        }

        public bool TryFind(ItemDataPredicate itemDataPredicate, out InventorySlot slot)
        {
            var span = _slots.AsSpan();
            var length = _capacity;

            for (int i = 0; i < length; i++)
            {
                slot = span[i];

                if (slot == null)
                    continue;

                if (itemDataPredicate(slot.Item))
                    return true;
            }

            slot = null;
            return false;
        }

        public bool TryFind(SlotPredicate slotPredicate, out InventorySlot slot)
        {
            var span = _slots.AsSpan();
            var length = _capacity;

            for (int i = 0; i < length; i++)
            {
                slot = span[i];

                if (slot == null)
                    continue;

                if (slotPredicate(slot))
                    return true;
            }

            slot = null;
            return false;
        }

        public ReadOnlyMemory<InventorySlot> FindAll(SlotPredicate slotPredicate)
        {
            var span = _slots.AsSpan();
            var length = _capacity;

            int count = 0;

            for (int i = 0; i < length; i++)
            {
                var slot = span[i];

                if (slot == null)
                    continue;

                if (slotPredicate(slot))
                    _slotsBuffer[count++] = slot;
            }

            if (count > 0)
                return _slotsMem[..count];

            return ReadOnlyMemory<InventorySlot>.Empty;
        }

        public ReadOnlyMemory<InventorySlot> FindAllSlotsWithItemData(ItemData itemData)
        {
            var span = _slots.AsSpan();
            var length = _capacity;

            int count = 0;

            for (int i = 0; i < length; i++)
            {
                var slot = span[i];

                if (slot == null)
                    continue;

                if (slot.Item == itemData)
                    _slotsBuffer[count++] = slot;
            }

            if (count > 0)
                return _slotsMem[..count];

            return ReadOnlyMemory<InventorySlot>.Empty;
        }


        internal InventorySlot[] GetSlotsArray()
        {
            return _slots;
        }


        private void InitSlots(int start, int length)
        {
            for (int i = start; i < length; i++)
            {
                _slots[i] = new InventorySlot(i);
            }
        }


        //TODO: rename
        private void EmulateAddingStackable(ItemData itemData, ref int amountLeft, ref int instructionsCount)
        {
            var slotsOfKind = FindAllSlotsWithItemData(itemData).Span;
            int max = itemData.Reference.StackSize;

            foreach (var slot in slotsOfKind)
            {
                int canAdd = max - slot.Amount;

                if (canAdd > 0)
                {
                    int add = canAdd;

                    if (add > amountLeft)
                        add = amountLeft;

                    AddInstruction(ref instructionsCount, slot, itemData, slot.Amount + add);
                    amountLeft -= add;

                    if (amountLeft == 0)
                        return;
                }
            }

            EmulateAddingToEmptySlots(itemData, ref amountLeft, ref instructionsCount);
        }

        private void EmulateAddingToEmptySlots(ItemData itemData, ref int amountLeft, ref int instructionsCount)
        {
            var emptySlots = GetAllEmptySlots().Span;
            int max = itemData.Reference.StackSize;

            foreach (var slot in emptySlots)
            {
                int add = max;

                if (add > amountLeft)
                    add = amountLeft;

                AddInstruction(ref instructionsCount, slot, itemData, add);
                amountLeft -= add;

                if (amountLeft == 0)
                    return;
            }
        }

        private void AddInstruction(ref int instructionsCount, InventorySlot slot, ItemData itemData, int amount)
        {
            _precalculatedInstructionsBuffer[instructionsCount++].Set(slot, itemData, amount);
        }

        private void ExecuteInstructions(int instructionsCount)
        {
            var instructions = _precalculatedInstructionsMem[..instructionsCount].Span;

            foreach (var instruction in instructions)
                instruction.Execute();
        }

        private void DisposeUnusedInstructions(int instructionsCount)
        {
            var instructions = _precalculatedInstructionsMem[..instructionsCount].Span;

            foreach (var instruction in instructions)
                instruction.Dispose();
        }
        private ReadOnlyMemory<InventorySlot> GetAllEmptySlots()
        {
            var span = _slots.AsSpan();
            var length = _capacity;

            int count = 0;

            for (int i = 0; i < length; i++)
            {
                var slot = span[i];

                if (slot == null)
                    _slotsBuffer[count++] = slot;
            }

            if (count > 0)
                return _slotsMem[..count];

            return ReadOnlyMemory<InventorySlot>.Empty;
        }
    }
}
