using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewFishing
{
    public class BucketSpawn : MonoBehaviour
    {
        [SerializeField] private BucketPool _pool;
        [SerializeField] public GameObject spawnBucketPoint;

        //Spawn Bucket
        public Bucket SpawnBucket()
        {
            Bucket bucket = _pool.PoolBucket.GetFreeElement();
            bucket.transform.position = spawnBucketPoint.transform.position;
            bucket.Construct();
            return bucket;
        }
    }
}
