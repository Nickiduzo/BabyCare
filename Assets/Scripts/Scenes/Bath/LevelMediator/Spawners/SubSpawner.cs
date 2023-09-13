using Inputs;
using Sound;
using UnityEngine;

namespace Bath
{
    public class SubSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private SoundSystem _soundSystem;

        [SerializeField] private SubPool _pool;
        //Spawn submarine and initialize it
        public Toy SpawnSub()
        {
            Toy sub = _pool.Pool.GetFreeElement();
            sub.transform.position = _spawnPoint.position;
            sub.Construct(_inputSystem, _soundSystem, _spawnPoint.position);
            return sub;
        }
    }
}
