using Optimization;
using UnityEngine;

namespace Fishing.Spawners
{
    public class FishingBasketPool : MonoBehaviour
    {
        [SerializeField] private FishLevelConfig _config;
        public PoolMono<BasketPrefab> Pool { get; private set; }

        private void Awake()
            => Pool = new PoolMono<BasketPrefab>(_config.Basket, 1, false, false, transform);
    }
}