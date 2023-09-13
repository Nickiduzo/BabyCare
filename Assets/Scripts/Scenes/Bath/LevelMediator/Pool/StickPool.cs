using Optimization;
using UnityEngine;

namespace Bath
{
    public class StickPool : MonoBehaviour
    {
        [SerializeField] BathConfig _config;
        public PoolMono<BubbleStick> Pool { get; private set; }

        private void Awake()
            => Pool = new PoolMono<BubbleStick>(_config.Stick, 1, true, true, transform);
    }
}
