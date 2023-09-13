using System;
using System.Collections;
using UnityEngine;

namespace Sound
{
    public class SoundSystem : MonoBehaviour, ISoundSystem
    {
        [SerializeField] private Sound[] _sounds;
        [SerializeField] private Sound _music;

        private bool _isMusicOn;
        private bool _isSoundOn;

        private const string MUSIC_KEY = "isMusicOn";
        private const string SOUND_KEY = "isSoundOn";

        private void Awake()
        {
            InitSetting();
        }

        public void InitSetting()
        {
            _isMusicOn = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
            _isSoundOn = PlayerPrefs.GetInt(SOUND_KEY, 1) == 1;

            InitSounds();
            InitLevelMusic();
        }

        //Plays sound by name
        public void PlaySound(string name)
        {
            
            if (_isSoundOn)
            {
                Sound playSound = GetSoundByName(name);
                // якщо не знайде за назвою трек
                if (playSound == null){
                    Debug.LogError("sound null - " + name);
                    string list = "[";
                    foreach(Sound s in _sounds){
                        list += s.Name;
                        list += " , ";
                    }
                    list += "]";
                    Debug.Log(list);
                }
                playSound?.Source.Play();
            }
        }

        public void StopSound(string name)
        {
            if (_isSoundOn)
            {
                Sound playSound = GetSoundByName(name);
                playSound?.Source.Stop();
            }

        }

        public void StopLevelMusic(float fadeDuration)
        {
            StartCoroutine(FadeMusic(false, fadeDuration));
        }
        //Starts Level Music
        private void InitLevelMusic()
        {
            if (_isMusicOn)
            {
                Debug.Log("_isMusicOn: " + _isMusicOn);
                Debug.Log("_music.Source: " + _music.Source);

                if (_music.Source != null)
                {
                    _music.Source.Play();
                    return;
                }

                _music.Source = gameObject.AddComponent<AudioSource>();
                _music.Source.clip = _music.AudioClip;
                _music.Source.volume = _music.Volume;
                _music.Source.loop = true;
                _music.Source.Play();
            }
            else
            {
                if( _music.Source != null)
                {
                    _music?.Source.Stop();
                }
            }

        }

        private IEnumerator FadeMusic(bool fadeIn, float fadeTime)
        {
            Debug.Log("FadeMusic - _music.Source.isPlaying: " + _music.Source.isPlaying);
            float startVolume = _music.Source.volume;
            float endVolume = fadeIn ? _music.Volume : 0f;
            float t = 0f;

            while (t < fadeTime)
            {
                t += Time.deltaTime;
                _music.Source.volume = Mathf.Lerp(startVolume, endVolume, t / fadeTime);
                yield return null;
            }

            if (!fadeIn && _music.Source.isPlaying)
            {
                _music.Source.Stop();
            }
        }

        //Inits level sounds

        private void InitSounds()
        {
            if (_isSoundOn)
            {
                foreach (var sound in _sounds)
                {
                    if (sound.Source != null)
                        continue;;

                    sound.Source = gameObject.AddComponent<AudioSource>();
                    sound.Source.clip = sound.AudioClip;
                    sound.Source.volume = sound.Volume;
                    sound.Source.loop = sound.Loop;
                    sound.Source.pitch = sound.Pitch;
                }
            }
            else
            {
                foreach (var sound in _sounds)
                {
                    if (sound.Source != null)
                    {
                        sound?.Source.Stop();
                    }
                    
                }

            }
        }

        private Sound GetSoundByName(string name)
            => Array.Find(_sounds, sound => sound.Name == name);
    }
}