using Optimization;
using UnityEngine;

namespace PlayScene
{
    public class RobotPool : MonoBehaviour
    {
        [SerializeField] private PlaySceneController _config;
        public PoolMono<Robot> PoolRobot { get; private set; }

        // get Obgect from config for pool, set number of Obgect that will appear
        private void Awake()
            => PoolRobot = new PoolMono<Robot>(_config.robot, 1, true, true, transform);
    }
}
