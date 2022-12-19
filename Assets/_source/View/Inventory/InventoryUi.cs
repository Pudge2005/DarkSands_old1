using System;
using System.Collections;
using System.Collections.Generic;
using Game.Core.ItemsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

namespace Game.View
{
    public class InventorySlotUi : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Image _iconImg;

        private InventoryUi _parent;
        private ItemData _containingItem;


        internal void InitSlot(InventoryUi parent, ItemData itemData)
        {
            _parent = parent;
            _containingItem = itemData;

            UpdateView();
        }


        public void UpdateView()
        {
            var reference = _containingItem.Reference;
            _amountText.text = "x";
            _iconImg.sprite = reference.MetaInfo.Icon;
        }


        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
        }
    }
    public class InventoryUi : MonoBehaviour
    {
      
    }
}
