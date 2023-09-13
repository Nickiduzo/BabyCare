using Optimization;
using UnityEngine;

namespace Feeding
{
    public class PorridgePool : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _config;
        public PoolMono<Porridge> PoolPorridge { get; private set; }

        // get Porridge from config for pool, set number of Porridge that will appear
        private void Awake()
            => PoolPorridge = new PoolMono<Porridge>(_config.porridge, 1, true, true, transform);
    }
}
