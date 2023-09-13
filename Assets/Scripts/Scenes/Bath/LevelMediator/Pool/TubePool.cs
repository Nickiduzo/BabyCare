using Optimization;
using UnityEngine;

namespace Bath
{
    public class TubePool : MonoBehaviour
    {
        [SerializeField] BathConfig _config;
        public PoolMono<Tube> Pool { get; private set; }

        private void Awake()
            => Pool = new PoolMono<Tube>(_config.Tube, 1, true, true, transform);
    }
}
