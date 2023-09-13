using UnityEngine;

namespace Dream
{
    [RequireComponent(typeof(Animator), typeof(MouseTrigger))]
    public class ToyBunny : MonoBehaviour
    {
        [SerializeField] private string _shakingAnimation = "Shaking";
        private Animator _animator;
        private bool _shaking;

        //Just an NPC. Playing a simple anim when player press on it.

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            var mouseTrigger = GetComponent<MouseTrigger>();
            mouseTrigger.OnUp += Shaking;
        }

        //Invoking when player press on Bunny
        private void Shaking()
        {
            if (!_shaking)
            {
                _animator.SetTrigger(_shakingAnimation);
                _shaking = true;
            }
        }

        //Making bunny interactable again. Invoking in Animation "Moving"
        public void ToggleShaking()
        {
            _shaking = false;
        }
    }
}