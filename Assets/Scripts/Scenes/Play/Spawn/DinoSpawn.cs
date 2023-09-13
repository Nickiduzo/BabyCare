using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene
{
    public class DinoSpawn : MonoBehaviour
    {
        [SerializeField] private DinoPool _pool;
        [SerializeField] private GameObject spawnDinoPoint;

        //Spawn Object
        public Dino SpawnDino()
        {
            Dino spawnobject = _pool.PoolDino.GetFreeElement();
            spawnobject.transform.position = spawnDinoPoint.transform.position;
            spawnobject.Construct();
            return spawnobject;
        }
    }
}
