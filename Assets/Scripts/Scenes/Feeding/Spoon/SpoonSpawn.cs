using Inputs;
using Sound;
using System;
using UnityEngine;

namespace Feeding
{
    public class SpoonSpawn : MonoBehaviour
    {
        public event Action<Spoon> OnSpawn;

        [SerializeField] private SpoonPool _pool;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private SoundSystem _soundSystem;

        //Spawn Spoon
        public Spoon SpawnSpoon()
        {
            Spoon spoon = _pool.PoolSpoon.GetFreeElement();
            spoon.transform.position = _spawnPoint.position;
            spoon.Construct(_inputSystem, _soundSystem);
            OnSpawn?.Invoke(spoon);
            return spoon;
        }
    }
}
