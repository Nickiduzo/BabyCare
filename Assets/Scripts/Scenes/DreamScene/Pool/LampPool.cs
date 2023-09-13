using Optimization;
using UnityEngine;

namespace Dream
{
    public class LampPool : MonoBehaviour
    {
        [SerializeField] private DreamSceneConfig _config;
        public PoolMono<Lamp> Pool { get; private set; }

        //Initialize lamp pool
        private void Awake()
            => Pool = new PoolMono<Lamp>(_config.Lamp, 1, true, true, transform);
    }
}
