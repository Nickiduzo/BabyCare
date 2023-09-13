using Optimization;
using UnityEngine;

namespace PlayScene
{
    public class BeanbagPool : MonoBehaviour
    {
        [SerializeField] private PlaySceneController _config;
        public PoolMono<Beanbag> PoolBeanbag { get; private set; }

        // get Obgect from config for pool, set number of Obgect that will appear
        private void Awake()
            => PoolBeanbag = new PoolMono<Beanbag>(_config.beanbag, 1, true, true, transform);
    }
}
