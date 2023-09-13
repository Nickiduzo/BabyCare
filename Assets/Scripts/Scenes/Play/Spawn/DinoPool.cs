using Optimization;
using UnityEngine;
namespace PlayScene
{
    public class DinoPool : MonoBehaviour
    {
        [SerializeField] private PlaySceneController _config;
        public PoolMono<Dino> PoolDino { get; private set; }

        // get Obgect from config for pool, set number of Obgect that will appear
        private void Awake()
            => PoolDino = new PoolMono<Dino>(_config.dino, 1, true, true, transform);
    }
}
