using Inputs;
using Sound;
using UnityEngine;

namespace Dream
{
    public class BlanketSpawner : MonoBehaviour
    {
        [SerializeField] private BlanketPool _pool;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private Transform _destination;
        [SerializeField] private Transform _spawnPoint;

        //Spawn and construct Blanket
        public Blanket SpawnBlanket()
        {
            Blanket blanket = _pool.Pool.GetFreeElement();
            blanket.transform.position = _spawnPoint.position;
            blanket.Construct(_inputSystem, _soundSystem, _destination, _spawnPoint.position);
            return blanket;
        }
    }
}
