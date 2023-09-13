using Optimization;
using UnityEngine;

namespace Feeding
{

    public class BlobsPool : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _config;
        public PoolMono<Blobs> PoolBlobs { get; private set; }

        // get Blob from config for pool, set number of blob that will appear
        private void Awake()
            => PoolBlobs = new PoolMono<Blobs>(_config.blobs, 1, true, true, transform);
    }
}
