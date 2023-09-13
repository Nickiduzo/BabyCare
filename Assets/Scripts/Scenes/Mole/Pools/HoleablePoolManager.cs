using Mole.Config;
using Optimization;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Mole.Spawners
{
    public class HoleablePoolManager : MonoBehaviour
    {
        [SerializeField] private CarrotLevelConfig _config;

        private List<PoolMono<BaseHoleable>> _pools = new();
        public ReadOnlyCollection<PoolMono<BaseHoleable>> HoleablePools => _pools.AsReadOnly();
        public PoolMono<FruitSplash> SplashPool { get; private set; }

        // get [Mole] from config fro pool, get number of moles that will appear from config 
        private void Awake()
        {
            foreach(var prefab in _config.Prefabs)
            {
                PoolMono<BaseHoleable> pool = new(prefab, _config.MaxMoleToSpawn + 1, true, true, transform);
                _pools.Add(pool);
            }
            SplashPool = new(_config.Splash, _config.MaxSplashToSpawn, true, true, transform);
        }
    }
}