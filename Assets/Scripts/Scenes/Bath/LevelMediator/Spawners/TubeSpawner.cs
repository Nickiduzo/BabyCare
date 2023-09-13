using Inputs;
using Sound;
using UnityEngine;

namespace Bath
{
    public class TubeSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private SoundSystem _soundSystem;

        [SerializeField] private TubePool _pool;
        //Initialize Tude for mediator
        public Tube SpawnTube()
        {
            Tube tube = _pool.Pool.GetFreeElement();
            tube.Construct(_soundSystem);
            tube.transform.position = _spawnPoint.position;
            return tube;
        }
    }
}
