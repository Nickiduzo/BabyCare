using Optimization;
using UnityEngine;

namespace Dream
{
    public class BlanketPool : MonoBehaviour
    {
        [SerializeField] private DreamSceneConfig _config;
        public PoolMono<Blanket> Pool { get; private set; }

        //Initialize blanket pool
        private void Awake()
            => Pool = new PoolMono<Blanket>(_config.Blanket, 1, true, true, transform);
    }
}
