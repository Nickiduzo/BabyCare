using Optimization;
using UnityEngine;

namespace Thimble
{
    public class MagicianPool : MonoBehaviour
    {
        [SerializeField] private ThimbleSceneConfig _config;
        public PoolMono<Magician> PoolMagician { get; private set; }

        // get Magician from config for pool, set number of magician that will appear
        private void Awake()
            => PoolMagician = new PoolMono<Magician>(_config.magician, 1, true, true, transform);
    }
}
