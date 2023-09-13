using Optimization;
using System.Collections.Generic;
using UnityEngine;


namespace NewFishing
{
    public class FishPool : MonoBehaviour
    {
        [SerializeField] private NewFishingController _config;
        public PoolMono<FishConstracror> PoolFish { get; private set; }

        // get Fish from config for pool, set number of Fish that will appear
        private void Awake()
        {
            PoolFish = new PoolMono<FishConstracror>(_config.fish, 7, true, true, transform);
        }
        //Again get Fish from config for pool, set number of Fish that will appear 
        public void SpawnAgain()
        {
            PoolFish = new PoolMono<FishConstracror>(_config.fish, 7, true, true, transform);
        }

    }
}
