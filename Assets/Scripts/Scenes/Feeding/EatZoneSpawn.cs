using UnityEngine;

namespace Feeding
{
    public class EatZoneSpawn : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private EatZonePool _pool;

        //Spawn EatZone
        public EatZone SpawnEatZone()
        {
            EatZone zone = _pool.PoolEatzone.GetFreeElement();
            zone.transform.position = _spawnPoint.position;
            zone.Construct();
            return zone;
        }
    }
}
