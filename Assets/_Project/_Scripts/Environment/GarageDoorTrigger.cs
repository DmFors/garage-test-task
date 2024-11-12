using StarterAssets;
using UnityEngine;

namespace Game.Environment
{
    public class GarageDoorTrigger : MonoBehaviour
    {
        [SerializeField] private GarageDoorController _doorController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FirstPersonController player))
            {
                _doorController.ToggleDoor();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out FirstPersonController player))
            {
                _doorController.ToggleDoor();
            }
        }
    }
}