using System;
using System.Linq;
using Sound;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO rework to not monoBehaviour, add preconditions, replace to another directory

public class RandomSound : MonoBehaviour, ISound
{
    [SerializeField] private SoundSystem _soundSystem;
    [SerializeField] private string[] _sounds;
    private string _activeSound;

    private void Awake()
    {
        if (_sounds.Any(string.IsNullOrEmpty))
            throw new NullReferenceException(nameof(_sounds));
    }

    public void Play()
    {
        _activeSound = _sounds[Random.Range(0, _sounds.Length)];
        _soundSystem.PlaySound(_activeSound);
    }

    public void Stop() =>
        _soundSystem.StopSound(_activeSound);
}