using Optimization;
using UnityEngine;

namespace NewFishing
{
    public class FishingRodPool : MonoBehaviour
    {
        [SerializeField] private NewFishingController _config;
        public PoolMono<FishingRod> PoolFishingRod { get; private set; }

        // get FishingRod from config for pool, set number of FishingRod that will appear
        private void Awake()
            => PoolFishingRod = new PoolMono<FishingRod>(_config.fishingRod, 1, true, true, transform);
    }
}
