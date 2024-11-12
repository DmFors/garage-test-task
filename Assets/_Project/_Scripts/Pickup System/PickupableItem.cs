using System;
using UnityEngine;

namespace Game.PickupSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class PickupableItem : MonoBehaviour
    {
        private Rigidbody _rb;
        private GameObject _holder;

        public bool IsHeld => _holder != null;

        public string Name => gameObject.name;

        private void Awake() => _rb = GetComponent<Rigidbody>();

        public void Pickup(GameObject holder)
        {
            if (IsHeld)
            {
                throw new InvalidOperationException($"Item {Name} is already held!");
            }

            this._holder = holder;
            _rb.isKinematic = true;
        }

        public void Drop()
        {
            _holder = null;
            _rb.isKinematic = false;
        }
    }
}
