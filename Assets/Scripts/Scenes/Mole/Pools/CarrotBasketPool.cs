using Mole.Config;
using Optimization;
using UnityEngine;

namespace Mole.Spawners
{
    public class CarrotBasketPool : MonoBehaviour
    {
        [SerializeField] private CarrotLevelConfig _config;
        public PoolMono<BasketPrefab> Pool { get; private set; }

        // get [BasketPrefab] from config for pool
        private void Awake()
            => Pool = new PoolMono<BasketPrefab>(_config.BasketPrefab, 1, false, false, transform);
    }
}