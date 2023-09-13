using Optimization;
using UnityEngine;

namespace Thimble
{
    public class ThimbeCupPool : MonoBehaviour
    {
        [SerializeField] private ThimbleSceneConfig _config;
        public PoolMono<ThimbleCup> PoolCup { get; private set; }

        // get Cup from config for pool, set number of cup that will appear
        private void Awake()
            => PoolCup = new PoolMono<ThimbleCup>(_config.thimblecup, 3, true, true, transform);
    }
}
