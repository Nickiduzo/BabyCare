using Optimization;
using UnityEngine;

namespace Feeding
{
    public class NapkinPool : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _config;
        public PoolMono<Napkin> PoolNapkin { get; private set; }

        // get Napkin from config for pool, set number of Napkin that will appear
        private void Awake()
            => PoolNapkin = new PoolMono<Napkin>(_config.napkin, 1, true, false, transform);
    }
}
