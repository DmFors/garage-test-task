using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PickupSystem
{
    public class DropZoneController : MonoBehaviour
    {
        public event Action<PickupController> EnteredDropZone = delegate { };
        public event Action<PickupController> LeftDropZone = delegate { };
        public event Action NoSpaceInPickup = delegate { };

        [SerializeField] private List<Transform> itemPositions;
        private int currentItemPositionIndex = 0;
        private PickupController pickupController;
        private bool IsContainerFull => currentItemPositionIndex >= itemPositions.Count;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PickupController player) && player.IsHoldingItem)
            {
                EnterDropZone(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PickupController player) && player == pickupController)
            {
                ExitDropZone();
            }
        }

        private void Update()
        {
            if (pickupController != null && Input.GetKeyDown(KeyCode.E))
            {
                TryPlaceItem(pickupController);
            }
        }

        private void EnterDropZone(PickupController player)
        {
            pickupController = player;
            EnteredDropZone(pickupController);
        }

        private void ExitDropZone()
        {
            var exitingController = pickupController;
            pickupController = null;
            LeftDropZone(exitingController);
        }

        private bool TryPlaceItem(PickupController player)
        {
            if (!player.IsHoldingItem || IsContainerFull)
            {
                if (IsContainerFull) NoSpaceInPickup();
                return false;
            }

            var item = player.CurrentItem;
            player.DropItem();
            PlaceItem(item);
            currentItemPositionIndex++;

            return true;
        }

        private void PlaceItem(PickupableItem item)
        {
            item.Pickup(gameObject);
            item.transform.SetParent(itemPositions[currentItemPositionIndex]);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }
    }
}