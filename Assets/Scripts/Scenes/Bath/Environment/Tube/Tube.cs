using Sound;
using System;
using UnityEngine;

namespace Bath
{
    public class Tube : MonoBehaviour
    {
        public event Action OnWashingBaby;
        public event Action OnStopWashingBaby;

        private SoundSystem _sound;

        [SerializeField] private Crane _crane;
        [SerializeField] private ParticleSystem _particleSystem;
        //Main constructor for pool 
        public void Construct(SoundSystem sound)
        {
            _sound = sound;
            _crane.Construct(sound);
            _crane.OnTurnOnTube += TurnOnWater;
            _crane.OnTurnOffTube += TurnOffWater;
        }

        //Starting water particle and invoking baby method to start animation
        private void TurnOnWater()
        {
            _sound.PlaySound("Water");
            _particleSystem.Play();
            OnWashingBaby?.Invoke();
        }

        //Stopping water particle and invoking baby method to stop animation
        private void TurnOffWater()
        {
            _sound.StopSound("Water");
            _particleSystem.Stop();
            OnStopWashingBaby?.Invoke();
        }
    }
}
