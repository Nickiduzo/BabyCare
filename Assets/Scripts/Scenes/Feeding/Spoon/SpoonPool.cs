using Optimization;
using UnityEngine;


namespace Feeding
{
    public class SpoonPool : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _config;
        public PoolMono<Spoon> PoolSpoon { get; private set; }

        // get Spoon from config for pool, set number of Spoon that will appear
        private void Awake()
            => PoolSpoon = new PoolMono<Spoon>(_config.spoon,4, true, false, transform);
    }
}
