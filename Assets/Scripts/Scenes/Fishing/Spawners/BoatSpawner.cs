using Inputs;
using Sound;
using System;
using UnityEngine;

namespace Fishing.Spawners
{
    public class BoatSpawner : MonoBehaviour
    {
        public event Action<Hook, Animator> OnSpawn;

        [SerializeField] private FishLevelConfig _config;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private Camera _camera;


        public void SpawnBoat()
        {
            var destination = _camera.ScreenToWorldPoint(new Vector3(0.33f * Screen.width, 0.6f* Screen.height, 1));

            GameObject boat = Instantiate(_config.Boat, destination, Quaternion.identity);
            Animator animator = boat.GetComponent<Animator>();
            Hook hook = boat.GetComponentInChildren<Hook>();
            hook.Construct(_inputSystem, _soundSystem, animator);
            OnSpawn?.Invoke(hook, animator);
        }
    }
}