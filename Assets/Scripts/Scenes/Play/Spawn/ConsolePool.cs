using Optimization;
using UnityEngine;

namespace PlayScene
{
    public class ConsolePool : MonoBehaviour
    {
        [SerializeField] private PlaySceneController _config;
        public PoolMono<Console> PoolConsole { get; private set; }

        // get Obgect from config for pool, set number of Obgect that will appear
        private void Awake()
            => PoolConsole = new PoolMono<Console>(_config.console, 1, true, true, transform);
    }
}
