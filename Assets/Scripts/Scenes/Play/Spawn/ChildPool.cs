using Optimization;
using UnityEngine;

namespace PlayScene
{
    public class ChildPool : MonoBehaviour
    {
        [SerializeField] private PlaySceneController _config;
        public PoolMono<Child> PoolChild { get; private set; }

        // get Obgect from config for pool, set number of Obgect that will appear
        private void Awake()
            => PoolChild = new PoolMono<Child>(_config.child, 1, true, true, transform);
    }
}
