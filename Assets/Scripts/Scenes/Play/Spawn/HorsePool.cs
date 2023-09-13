using Optimization;
using UnityEngine;

namespace PlayScene
{
    public class HorsePool : MonoBehaviour
    {
        [SerializeField] private PlaySceneController _config;
        public PoolMono<Horse> PoolHorse { get; private set; }

        // get Obgect from config for pool, set number of Obgect that will appear
        private void Awake()
            => PoolHorse = new PoolMono<Horse>(_config.hourse, 1, true, true, transform);
    }
}
