using System;
using UnityEngine;

namespace Game.PickupSystem
{
    public class PickupController : MonoBehaviour
    {
        public event Action<PickupableItem> ItemFocused = delegate { };
        public event Action<PickupableItem> ItemDefocused = delegate { };
        public event Action<PickupableItem> ItemPickedUp = delegate { };
        public event Action<PickupableItem> ItemDropped = delegate { };

        [SerializeField] private ItemScanner _itemScanner;
        [SerializeField] private Transform _holdItemPosition;

        public bool IsHoldingItem => CurrentItem != null;
        public PickupableItem CurrentItem { get; private set; }

        private void OnEnable()
        {
            _itemScanner.ItemFocused += OnItemFocusedHandler;
            _itemScanner.ItemDefocused += OnItemDefocusedHandler;
        }

        private void OnDisable()
        {
            _itemScanner.ItemFocused -= OnItemFocusedHandler;
            _itemScanner.ItemDefocused -= OnItemDefocusedHandler;
        }

        private void OnItemFocusedHandler(PickupableItem item)
        {
            if (!IsHoldingItem)
            {
                ItemFocused(item);
            }
        }

        private void OnItemDefocusedHandler(PickupableItem item)
        {
            if (!IsHoldingItem)
            {
                ItemDefocused(item);
            }
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.E) && _itemScanner.FocusedItem != null)
            {
                PickupItem(_itemScanner.FocusedItem);
            }
            else if (Input.GetKeyDown(KeyCode.Q) && IsHoldingItem)
            {
                DropItem();
            }
        }

        public void PickupItem(PickupableItem item)
        {
            CurrentItem = item;
            item.Pickup(gameObject);
            item.transform.SetParent(_holdItemPosition);
            item.transform.localPosition = Vector3.zero;
            _itemScanner.Deactivate();

            ItemPickedUp(CurrentItem);
        }

        public void DropItem()
        {
            if (CurrentItem == null) return;

            var droppedItem = CurrentItem;
            CurrentItem.Drop();
            CurrentItem.transform.SetParent(null);
            CurrentItem = null;
            _itemScanner.Activate();

            ItemDropped(droppedItem);
        }
    }
}