using Optimization;
using UnityEngine;

namespace ShootingGallery
{
    public class BallShootPool : MonoBehaviour
    {
        [SerializeField] private ShootingGallerySceneControler _config;
        public PoolMono<BallShoot> PoolBallShoot { get; private set; }

        // get Ball from config for pool, set number of Ball that will appear
        private void Awake()
            => PoolBallShoot = new PoolMono<BallShoot>(_config.ballShoot, 1, true, true, transform);
    }
}
