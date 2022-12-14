using System;
using System.Collections;
using System.Collections.Generic;
using DevourDev.Base.Numerics;

namespace DevourDev.Base.Collections.Generic
{
    /// <summary>
    /// Если количество Предмета стало равно нулю - Предмет удаляется.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CountingDictionary<T> : IEnumerable<KeyValuePair<T, RefInt>>, IEnumerable
    {
        private readonly Dictionary<T, RefInt> _items;
        private readonly bool _allowNegativeValues;


        public CountingDictionary(bool allowNegativeValues = false, int capacity = 0)
        {
            _items = new(capacity);
            _allowNegativeValues = allowNegativeValues;
        }


        public int Count => _items.Count;


        public bool Contains(T item) => _items.ContainsKey(item);

        public bool TryGetAmount(T item, out int amount)
        {
            if (!TryGetAmountRef(item, out var amref))
            {
                amount = -1;
                return false;
            }

            amount = amref.Value;
            return true;
        }

        public bool TryGetAmountRef(T item, out RefInt amountRef) => _items.TryGetValue(item, out amountRef);


        /// <param name="initialAmount">can be negative if
        /// _allowNegativeValues</param>
        /// <exception cref="System.ArgumentException">
        /// Элемент с таким ключом уже существует в
        /// коллекции CountingDictionary</exception> 
        public void AddItem(T item, int initialAmount)
        {
            _items.Add(item, new(initialAmount));
        }

        /// <param name="initialAmount">can be negative if
        /// _allowNegativeValues</param>
        public bool TryAddItem(T item, int initialAmount)
        {
            if (Contains(item))
                return false;

            AddItem(item, initialAmount);
            return true;
        }


        public bool TryRemoveItem(T item)
        {
            return _items.Remove(item);
        }


        /// <summary>
        /// Если Предмета не существует
        /// в коллекции - он в неё добавляется
        /// с <paramref name="amount"/> в качестве
        /// своего количества, если <paramref name="amount"/>
        /// больше нуля. Если равно 0 - ничего не происходит.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <exception cref="System.ArgumentOutOfRangeException">попытка
        /// добавить отрицательное значение</exception>
        public void AddAmount(T item, int amount)
        {
            if (amount == 0)
                return;

#if DEVOUR_DEBUG || UNITY_EDITOR
            if (amount < 0)
                throw new System.ArgumentOutOfRangeException($"amount should be >= 0 ({amount})");
#endif

            if (TryGetAmountRef(item, out var amountRef))
            {
                amountRef.Value += amount;
                return;
            }

            AddItem(item, amount);
        }

        ///<summary>
        /// Не добавляет предмет в коллекцию, если
        /// он в ней не существует - вместо этого
        /// возвращает false
        ///</summary>
        /// <returns>false if key not found</returns>
        public bool TryAddAmount(T item, int amount)
        {
            if (TryGetAmountRef(item, out var amountRef))
            {
                amountRef.Value += amount;
                return true;
            }

            return false;
        }


        public void RemoveAmount(T item, int amountToRemove)
        {
            //обращение без проверок для генерации исключений
            //сначала выполняется проверка на наличие ключа,
            //а потом на количество - иначе попытка неправильного
            //вызова не будет обнаружена
            var amtRef = _items[item];

            if (amountToRemove == 0)
                return;

#if DEVOUR_DEBUG || UNITY_EDITOR
            if (amountToRemove < 0)
                throw new System.ArgumentOutOfRangeException($"amount should be >= 0 ({amountToRemove})");
#endif

            int newVal = amtRef.Value - amountToRemove;

            if (!_allowNegativeValues && newVal < 0)
                throw new System.ArgumentOutOfRangeException($"trying to remove {amountToRemove} while" +
                    $" item {item} has only {amtRef.Value} amount");

            if (newVal == 0)
            {
                TryRemoveItem(item);
                return;
            }

            amtRef.Value = newVal;
        }

        public int CanRemoveAmount(T item, int amountToRemove)
        {
            if (!TryGetAmountRef(item, out var amtRef))
                return -1;

            if (_allowNegativeValues)
                return amountToRemove;

            int canRemove = amtRef.Value;

            if (amountToRemove < canRemove)
                canRemove = amountToRemove;

            return canRemove;
        }

        /// <summary>
        /// Если предмет обнаружен и его
        /// количество больше или равно
        /// <paramref name="amountToRemove"/>, либо,
        /// если для коллекции допустимы отрицательные значения,
        /// уменьшает его количество на <paramref
        /// name="amountToRemove"/>
        /// </summary>
        /// <returns>true, если предмет обнаружен и его
        /// количество было уменьшено на <paramref
        /// name="amountToRemove"/></returns>
        public bool TryRemoveExactAmount(T item, int amountToRemove)
        {
            if (!TryGetAmountRef(item, out var amtRef))
                return false;

            int newVal = amtRef.Value - amountToRemove;

            if (newVal == 0)
                TryRemoveItem(item);
            else if (_allowNegativeValues || newVal > 0)
                amtRef.Value = newVal;
            else
                return false;

            return true;

        }


        /// <summary>
        /// Уменьшает количество предмета на <paramref
        /// name="amountToRemove"/>, если количество
        /// предмета больше или равно <paramref
        /// name="amountToRemove"/>, либо при _allowNegativeAmount,
        /// в противном случае удаляет предмет (убирает всё кол-во).
        /// Если предмет не обнаружен - ничего не делает.
        /// </summary>
        /// <returns>Amount of removed items. -1
        /// if item was not found</returns>
        public int RemoveAmountOrAny(T item, int amountToRemove)
        {
            if (!TryGetAmountRef(item, out var amtRef))
                return -1;

            var newVal = amtRef.Value - amountToRemove;

            if (newVal == 0)
            {
                TryRemoveItem(item);
            }
            else if (_allowNegativeValues || newVal > 0)
            {
                amtRef.Value = newVal;
            }
            else
            {
                TryRemoveItem(item);
                return amtRef.Value;
            }

            return amountToRemove;

        }


        public bool TryFindItem(Predicate<T> predicate, out T result)
        {
            var collection = _items.Keys;

            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    result = item;
                    return true;
                }
            }

            result = default!;
            return false;
        }

        public bool TryFindItem(System.Func<T, int, bool> predicate, out T foundItem, out int itemAmount)
        {
            foreach (var kvp in _items)
            {
                if (predicate(kvp.Key, kvp.Value.Value))
                {
                    foundItem = kvp.Key;
                    itemAmount = kvp.Value.Value;
                    return true;
                }
            }

            foundItem = default!;
            itemAmount = -1;
            return false;
        }


        public Dictionary<T, RefInt>.KeyCollection GetContainingItems()
        {
            return _items.Keys;
        }


        IEnumerator<KeyValuePair<T, RefInt>> IEnumerable<KeyValuePair<T, RefInt>>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }


        public void Clear()
        {
            _items.Clear();
        }
    }
}
