using Sound;
using UnityEngine;

namespace Bath
{
    public class DuckSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private SoundSystem _soundSystem;

        [SerializeField] private DuckPool _pool;
        //Main spawn constructor for mediator
        public Duck SpawnDuck()
        {
            Duck duck = _pool.Pool.GetFreeElement();
            duck.Construct(_soundSystem);
            duck.transform.position = _spawnPoint.position;
            return duck;
        }
    }
}
