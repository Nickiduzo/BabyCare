using Optimization;
using UnityEngine;

namespace Bath
{
    public class DuckPool : MonoBehaviour
    {
        [SerializeField] BathConfig _config;
        public PoolMono<Duck> Pool { get; private set; }

        private void Awake()
            => Pool = new PoolMono<Duck>(_config.Duck, 1, true, true, transform);
    }
}
