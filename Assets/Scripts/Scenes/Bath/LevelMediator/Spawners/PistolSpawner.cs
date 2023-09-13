using Inputs;
using Sound;
using UnityEngine;

namespace Bath
{
    public class PistolSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private SoundSystem _soundSystem;

        [SerializeField] private PistolPool _pool;
        //Initialize and spawn pistol
        public Toy SpawnPistol()
        {
            Toy pistol = _pool.Pool.GetFreeElement();
            pistol.transform.position = _spawnPoint.position;
            pistol.Construct(_inputSystem, _soundSystem, _spawnPoint.position);
            return pistol;
        }
    }
}
