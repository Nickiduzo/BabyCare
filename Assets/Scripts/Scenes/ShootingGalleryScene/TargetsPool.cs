using Optimization;
using UnityEngine;

namespace ShootingGallery
{
    public class TargetsPool : MonoBehaviour
    {
        [SerializeField] private ShootingGallerySceneControler _config;
        public PoolMono<Targets> PoolTargets { get; private set; }

        // get Blob from config for pool, set number of blob that will appear
        private void Awake()
            => PoolTargets = new PoolMono<Targets>(_config.targets, 14, true, true, transform);
    }
}
