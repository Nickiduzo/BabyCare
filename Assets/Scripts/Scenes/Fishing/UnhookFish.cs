using UnityEngine;

namespace Fishing
{
    public class UnhookFish : MonoBehaviour
    {
        [SerializeField] private RandomMover _mover;

        public void Hooked()
            => _mover.enabled = false;

        public void UnHooked()
        {
            _mover.enabled = true;
        }
    }
}