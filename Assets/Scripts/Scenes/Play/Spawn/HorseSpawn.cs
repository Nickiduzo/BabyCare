using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene
{
    public class HorseSpawn : MonoBehaviour
    {
        [SerializeField] private HorsePool _pool;
        [SerializeField] private GameObject spawnHorsePoint;

        //Spawn Object
        public Horse SpawnHorse()
        {
            Horse spawnobject = _pool.PoolHorse.GetFreeElement();
            spawnobject.transform.position = spawnHorsePoint.transform.position;
            spawnobject.Construct();
            return spawnobject;
        }
    }
}
