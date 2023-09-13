using Optimization;
using UnityEngine;


namespace Feeding
{
    public class ApplePool : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _config;
        public PoolMono<Apple> PoolApple { get; private set; }

        // get Apple from config for pool, set number of apples that will appear
        private void Awake()
            => PoolApple = new PoolMono<Apple>(_config.apple, 1, true, true, transform);
    }
}
