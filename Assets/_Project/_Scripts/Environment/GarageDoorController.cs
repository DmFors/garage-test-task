using UnityEngine;

namespace Game.Environment
{
    [RequireComponent(typeof(Animator))]
    public class GarageDoorController : MonoBehaviour
    {
        private Animator _animator;
        private bool _isDoorOpen;

        private void Awake() => _animator = GetComponent<Animator>();

        public void ToggleDoor()
        {
            _isDoorOpen = !_isDoorOpen;
            _animator.SetBool("IsOpen", _isDoorOpen);
        }
    }
}