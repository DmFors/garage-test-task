using Game.PickupSystem;
using Game.UI.View;
using UnityEngine;

namespace Game.UI.Controller
{
    public class HintManager : MonoBehaviour
    {
        [SerializeField] private HintView _hintView;
        [SerializeField] private PickupController _pickupController;
        [SerializeField] private DropZoneController _dropZoneController;

        private void OnEnable()
        {
            _pickupController.ItemFocused += OnItemFocused;
            _pickupController.ItemDefocused += OnItemDefocused;
            _pickupController.ItemPickedUp += OnItemPickedUp;
            _pickupController.ItemDropped += OnItemDropped;
            _dropZoneController.NoSpaceInPickup += ShowNoSpaceHint;
            _dropZoneController.EnteredDropZone += OnPlayerEnteredDropZone;
            _dropZoneController.LeftDropZone += OnPlayerLeftDropZone;
        }

        private void OnDisable()
        {
            _pickupController.ItemFocused -= OnItemFocused;
            _pickupController.ItemDefocused -= OnItemDefocused;
            _pickupController.ItemPickedUp -= OnItemPickedUp;
            _pickupController.ItemDropped -= OnItemDropped;
            _dropZoneController.NoSpaceInPickup -= ShowNoSpaceHint;
            _dropZoneController.EnteredDropZone -= OnPlayerEnteredDropZone;
            _dropZoneController.LeftDropZone -= OnPlayerLeftDropZone;
        }

        private void ShowNoSpaceHint() => _hintView.ShowHint("Pickup Truck is full");

        private void OnItemFocused(PickupableItem item)
        {
            if (item != null)
            {
                _hintView.ShowHint($"Press E to PICK UP <b>{item.Name}</b>");
            }
        }

        private void OnItemDefocused(PickupableItem item) => _hintView.HideHint();

        private void OnItemPickedUp(PickupableItem item)
        {
            if (item != null)
            {
                _hintView.ShowHint($"Press Q to DROP <b>{item.Name}</b>");
            }
        }

        private void OnItemDropped(PickupableItem item) => _hintView.HideHint();

        private void OnPlayerEnteredDropZone(PickupController pickupController)
        {
            _hintView.ShowHint($"Press E to PLACE <b>{pickupController.CurrentItem.Name}</b> in the Pickup Truck");
        }

        private void OnPlayerLeftDropZone(PickupController pickupController) => _hintView.HideHint();
    }
}