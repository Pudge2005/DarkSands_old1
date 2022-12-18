using System.Collections;
using System.Collections.Generic;
using Game.Core.ItemsSystem;
using UnityEngine;

namespace Game.View
{
    public class InventorySlotUi : MonoBehaviour
    {
        //[SerializeField] private tmp
    }
    public class InventoryUi : MonoBehaviour
    {
        [SerializeField] private InventoryComponentBase<ItemSo> _inventory;


        private void Start()
        {
            _inventory.OnItemCountChanged += HandleInventoryChanged;
        }

        private void HandleInventoryChanged(IInventory<ItemSo> inventory, ItemSo item, int countDelta, int newCount)
        {

        }
    }
}
