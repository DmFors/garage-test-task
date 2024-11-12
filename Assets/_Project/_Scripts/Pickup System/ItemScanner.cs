using System;
using UnityEngine;

namespace Game.PickupSystem
{
    public class ItemScanner : MonoBehaviour
    {
        public event Action<PickupableItem> ItemFocused = delegate { };
        public event Action<PickupableItem> ItemDefocused = delegate { };

        [SerializeField] private float _pickupRange = 2.0f;
        [SerializeField] private Transform _eyeCameraPosition;
        [SerializeField] private LayerMask _holdItemLayer;

        private bool isScannerActive = true;

        public PickupableItem FocusedItem { get; private set; }

        private void Update()
        {
            if (!isScannerActive) return;

            PerformRaycast();
        }

        private void PerformRaycast()
        {
            if (Physics.Raycast(_eyeCameraPosition.position, _eyeCameraPosition.forward, out var hit, _pickupRange, _holdItemLayer))
            {
                if (hit.collider.TryGetComponent(out PickupableItem item) && !item.IsHeld)
                {
                    FocusItem(item);
                }
                else
                {
                    DefocusItem();
                }
            }
            else
            {
                DefocusItem();
            }
        }

        private void FocusItem(PickupableItem item)
        {
            if (item == FocusedItem) return;

            FocusedItem = item;
            ItemFocused(item);
        }

        private void DefocusItem()
        {
            if (FocusedItem == null) return;

            var defocusedItem = FocusedItem;
            FocusedItem = null;
            ItemDefocused(defocusedItem);
        }

        public void Activate() => isScannerActive = true;

        public void Deactivate()
        {
            DefocusItem();
            isScannerActive = false;
        }
    }
}