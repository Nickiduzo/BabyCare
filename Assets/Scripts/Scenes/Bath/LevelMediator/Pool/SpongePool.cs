using Optimization;
using UnityEngine;

namespace Bath
{
    public class SpongePool : MonoBehaviour
    {
        [SerializeField] BathConfig _config;
        public PoolMono<Sponge> Pool { get; private set; }

        private void Awake()
            => Pool = new PoolMono<Sponge>(_config.Sponge, 1, true, true, transform);
    }
}
