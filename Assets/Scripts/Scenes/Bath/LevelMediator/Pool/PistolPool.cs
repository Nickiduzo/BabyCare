using Optimization;
using UnityEngine;

namespace Bath
{
    public class PistolPool : MonoBehaviour
    {
        [SerializeField] BathConfig _config;
        public PoolMono<Toy> Pool { get; private set; }

        private void Awake()
            => Pool = new PoolMono<Toy>(_config.Pistol, 1, true, true, transform);
    }
}
