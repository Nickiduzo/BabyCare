using Sound;
using UnityEngine;

namespace Feeding
{
    public class BabyfeedingSpawn : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private BabyFeedengPool _pool;
        [SerializeField] private SoundSystem _soundSystem;

        //Spawn Baby
        public BabyFeeding SpawnBaby()
        {
            BabyFeeding baby = _pool.PoolBaby.GetFreeElement();
            baby.transform.position = _spawnPoint.position;
            baby.Construct(_soundSystem);
            return baby;
        }
    }
}
