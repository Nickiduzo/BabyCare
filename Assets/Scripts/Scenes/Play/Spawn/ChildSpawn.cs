using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayScene
{
    public class ChildSpawn : MonoBehaviour
    {
        [SerializeField] private ChildPool _pool;
        [SerializeField] private GameObject spawnChildPoint;

        //Spawn Object
        public Child SpawnChild()
        {
            Child spawnobject = _pool.PoolChild.GetFreeElement();
            spawnobject.transform.position = spawnChildPoint.transform.position;
            spawnobject.Construct();
            return spawnobject;
        }
    }
}
