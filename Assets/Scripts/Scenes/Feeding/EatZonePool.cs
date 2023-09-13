using System.Collections;
using System.Collections.Generic;
using Optimization;
using UnityEngine;


namespace Feeding
{
    public class EatZonePool : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _config;
        public PoolMono<EatZone> PoolEatzone { get; private set; }

        // get EatZone from config for pool, set number of EatZone that will appear
        private void Awake()
            => PoolEatzone = new PoolMono<EatZone>(_config.eatZone, 1, true, true, transform);
    }
}
