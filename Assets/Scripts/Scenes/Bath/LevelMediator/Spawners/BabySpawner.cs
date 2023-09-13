using Sound;
using UnityEngine;

namespace Bath
{
    public class BabySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private BabyPool _pool;
        //Spawn Baby by pool
        public Baby SpawnBaby()
        {
            Baby baby = _pool.Pool.GetFreeElement();
            baby.transform.position = _spawnPoint.position;
            baby.Construct(_soundSystem);
            return baby;
        }
    }
}
