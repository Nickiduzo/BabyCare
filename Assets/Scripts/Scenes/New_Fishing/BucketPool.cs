using Optimization;
using UnityEngine;


namespace NewFishing
{
    public class BucketPool : MonoBehaviour
    {

        [SerializeField] private NewFishingController _config;
        public PoolMono<Bucket> PoolBucket { get; private set; }

        // get Bucket from config for pool, set number of Bucket that will appear
        private void Awake()
            => PoolBucket = new PoolMono<Bucket>(_config.bucket, 1, true, true, transform);
    }
}
