using System;
using Sound;
using UnityEngine;

namespace Dream
{
    [RequireComponent(typeof(MouseTrigger))]
    public class Lamp : MonoBehaviour
    {
        public event Action OnTurnLampForBaby;
        public event Action OnTurningOffLight;

        [SerializeField] private GameObject _light;
        [SerializeField] private ParticleSystem _lava;
        private MouseTrigger _mouseTrigger;

        private SoundSystem _soundSystem;

        public void Construct(SoundSystem soundSystem)
        {
            if (soundSystem == null)
                throw new ArgumentNullException(nameof(soundSystem));

            _soundSystem = soundSystem;
            _mouseTrigger = GetComponent<MouseTrigger>();
            _mouseTrigger.OnUp += TurnOff;
        }

        //Turning off light
        private void TurnOff()
        {
            _light.SetActive(false);
            _lava.Stop();
            _soundSystem.PlaySound("Light");

            OnTurnLampForBaby?.Invoke();
            OnTurningOffLight?.Invoke();
            _mouseTrigger.OnUp -= TurnOff;
        }
    }
}