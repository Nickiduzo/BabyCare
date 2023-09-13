using Optimization;
using UnityEngine;

namespace Dream
{
    public class ToyWheelPool : MonoBehaviour
    {
        [SerializeField] private DreamSceneConfig _config;
        public PoolMono<ToyWheel> Pool { get; private set; }

        //Initialize toyWheel pool
        private void Awake()
            => Pool = new PoolMono<ToyWheel>(_config.ToyWheel, 1, true, true, transform);
    }
}
