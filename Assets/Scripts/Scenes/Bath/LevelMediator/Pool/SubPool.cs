using Optimization;
using UnityEngine;

namespace Bath
{
    public class SubPool : MonoBehaviour
    {
        [SerializeField] BathConfig _config;
        public PoolMono<Toy> Pool { get; private set; }

        private void Awake()
            => Pool = new PoolMono<Toy>(_config.Sub, 1, true, true, transform);
    }
}