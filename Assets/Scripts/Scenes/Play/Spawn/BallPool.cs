using Optimization;
using UnityEngine;


namespace PlayScene
{
    public class BallPool : MonoBehaviour
    {
        [SerializeField] private PlaySceneController _config;
        public PoolMono<Ball> PoolBall { get; private set; }

        // get Obgect from config for pool, set number of Obgect that will appear
        private void Awake()
            => PoolBall = new PoolMono<Ball>(_config.ball, 1, true, true, transform);
    }
}
