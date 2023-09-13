using System;
using Sound;
using UnityEngine;

namespace Dream
{
    [RequireComponent(typeof(Animator), typeof(MouseTrigger))]
    public class ToyWheel : MonoBehaviour
    {
        public event Action OnTurnOn;

        private Animator _animator;
        private SoundSystem _soundSystem;
        private bool _active;

        public void Construct(SoundSystem soundSystem)
        {
            if (soundSystem == null)
                throw new ArgumentNullException(nameof(soundSystem));
            
            _soundSystem = soundSystem;
            _animator = GetComponent<Animator>();

            var mouseTrigger = GetComponent<MouseTrigger>();
            mouseTrigger.OnUp += TurnOn;
        }

        //Turning on Toy Wheel
        private void TurnOn()
        {
            if (_active)
                return;

            _animator.SetTrigger("TurnOn");
            _soundSystem.PlaySound("WorkToys");
            OnTurnOn?.Invoke();
            _active = true;
        }

        //For animator. Invoking in animation TurningAround
        public void TurnOf()
        {
            if (!_active)
                return;

            _soundSystem.StopSound("WorkToys");
            _active = false;
        }
    }
}