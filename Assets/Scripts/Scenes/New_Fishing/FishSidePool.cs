using Optimization;
using UnityEngine;

namespace NewFishing
{
    public class FishSidePool : MonoBehaviour
    {
        [SerializeField] private NewFishingController _config;
        public PoolMono<FishSide> PoolFishSide { get; private set; }

        // get fishSide from config for pool, set number of fishSide that will appear
        private void Awake()
            => PoolFishSide = new PoolMono<FishSide>(_config.fishSide, 7, true, true, transform);
    }
}
