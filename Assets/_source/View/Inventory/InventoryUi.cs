using System;
using System.Linq;
using Game.Core.ItemsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.View
{

    public class InventorySlotUi : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField] private Image _iconImg;
        [SerializeField] private TMP_Text _amountText;

        private InventoryUi _parent;
        private int _id;
        private bool _cleared;
        private IInventorySlot _slot;


        public int ID => _id;


        private void OnEnable()
        {
            if (_slot != null)
                SubscribeAndActualize();
        }

        private void OnDisable()
        {
            if (_slot != null)
                _slot.OnChanged -= HandleSlotChanged;
        }

        internal void InitSlot(InventoryUi parent, int id, IInventorySlot slot)
        {
            _parent = parent;
            _id = id;
            _slot = slot;

            SubscribeAndActualize();
        }

        private void SubscribeAndActualize()
        {
            HandleSlotChanged(_slot, default, new(_slot));
            _slot.OnChanged += HandleSlotChanged;
        }

        private void HandleSlotChanged(IInventorySlot slot, ReadOnlyItemAmount prev, ReadOnlyItemAmount cur)
        {
            if (slot.Empty)
            {
                Clear();
                return;
            }

            EnsureNotCleared();
            var reference = cur.Item.Reference;
            _iconImg.sprite = reference.MetaInfo.Icon;
            _amountText.text = GetAmountText(slot);
        }

        private void Clear()
        {
            if (_cleared)
                return;

            _cleared = true;
            _iconImg.gameObject.SetActive(false);
            _amountText.gameObject.SetActive(false);
        }

        private void EnsureNotCleared()
        {
            if (!_cleared)
                return;

            _cleared = false;
            _iconImg.gameObject.SetActive(true);
            _amountText.gameObject.SetActive(true);
        }

        private string GetAmountText(IInventorySlot slot)
        {
            if (!slot.Item.Reference.Stackable)
                return string.Empty;

            return slot.Amount.ToString();
        }


        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_slot.Empty)
                return;

            //выполнить действие по-умолчанию для предмета в этом слоте
            //например: экипировку - надеть; зелье - выпить; корован - ограбить

            //возможно делегирование родительскому инвентарю (_parent) с
            //контекстом в виде индекса слота (_id) и инпутом пользователя
            //(клик, долгое нажатие, перетаскивание)
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _parent.ReportDragStart(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _parent.ReportDragEnd(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            _parent.ReportDrop(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }
    }
    public class DraggingItemUi : MonoBehaviour
    {
        [SerializeField] private Image _img;
    }

    public class InventoryUi : MonoBehaviour
    {
        [SerializeField] private InventorySlotUi _slotPrefab;
        [SerializeField] private Transform _slotsParent;

        //[SerializeField] private 
        [SerializeField] private InventoryComponent _inventory;

        private InventorySlotUi[] _slots;

        private InventorySlotUi _draggingSlot;


        private void Start()
        {
            BuildSlots();
        }

        private void BuildSlots()
        {
            if (_slots == null || _slots.Length < _inventory.Capacity)
                _slots = new InventorySlotUi[_inventory.Capacity];

            int i = -1;
            foreach (var sysSlot in _inventory)
            {
                var uiSlot = Instantiate(_slotPrefab, _slotsParent);
                uiSlot.InitSlot(this, ++i, sysSlot);
                _slots[i] = uiSlot;
            }
        }

        private void DestroySlots()
        {
            foreach (var uiSlot in _slots)
            {
                Destroy(uiSlot.gameObject);
            }

            var arr = _slots;
            var c = arr.Length;

            for (int i = 0; i < c; i++)
            {
                arr[i] = null;
            }
        }

        internal void ReportDragStart(InventorySlotUi slot)
        {
            _draggingSlot = slot;
        }

        internal void ReportDragEnd(InventorySlotUi slot)
        {
            _draggingSlot = null;
        }

        internal void ReportDrop(InventorySlotUi slot)
        {
            _inventory.SwapSlots(_draggingSlot.ID, slot.ID);
            _draggingSlot = null;
        }
    }
}
