using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sound;


namespace ShootingGallery
{
    public class BallMusic : MonoBehaviour
    {
        private SoundSystem _soundSystem;
        private void Awake()
        {
            _soundSystem = SoundSystemUser.Instance;
        }
        //Play different sounds
        public void PlaySound(string soundName)
        {
            _soundSystem.PlaySound(soundName);
        }
    }
}
