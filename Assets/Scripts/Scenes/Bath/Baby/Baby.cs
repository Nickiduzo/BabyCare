using Sound;
using System;
using UnityEngine;

namespace Bath
{
    public class Baby : MonoBehaviour
    {
        public event Action OnStopPlayingWithToys;

        [SerializeField] private Animator _animator;
        [SerializeField] private MouseTrigger _mouseTrigger;
        private SoundSystem _soundSystem;

        public bool IsPlaying;
        private bool _isBathing;

        public void Construct(SoundSystem soundSystem)
        {
            _soundSystem = soundSystem;
        }

        public void PlayWithPistol()
        {
            if (!IsPlaying)
            {
                if (_isBathing)
                    StopBathing();

                _animator.SetTrigger("PlayWithPistol");
                IsPlaying = true;
            }
        }

        public void PlayWithSubmarine()
        {
            if (!IsPlaying)
            {
                if (_isBathing)
                    StopBathing();

                _animator.SetTrigger("PlayWithSub");
                _soundSystem.PlaySound("Submarine");
                IsPlaying = true;
            }
        }

        public void StartSpongeBathing()
        {
            _animator.SetTrigger("Happy");
        }

        public void SpongeBathing()
        {
            _animator.SetTrigger("SpongeBathing");
        }

        public void StopSpongeBathing()
        {
            _soundSystem.PlaySound("StopSpongeBathing");
        }
        
        public void Bathing()
        {
            _animator.SetTrigger("Bathing");
            _isBathing = true;
        }

        public void StopBathing()
        {
            _animator.SetTrigger("StopBathing");
            _isBathing = false;
        }
        public void PlayWithThePistol()
        {
            _soundSystem.PlaySound("Pistol");
        }

        public void PlayIdea()
        {
            _soundSystem.PlaySound("Idea");
        }
        public void ResetAllStates()
        {
            OnStopPlayingWithToys?.Invoke();
            IsPlaying = false;
        }
    }
}
