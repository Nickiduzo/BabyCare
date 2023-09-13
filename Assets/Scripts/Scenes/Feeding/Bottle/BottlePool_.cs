using Optimization;
using UnityEngine;


namespace Feeding { 
    public class BottlePool_ : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _config;
        public PoolMono<Bottle> Pool { get; private set; }

        // get Bottle from config for pool, set number of bottle that will appear
        private void Awake()
            => Pool = new PoolMono<Bottle>(_config.bottle, 1, true, true, transform);
    }
}
