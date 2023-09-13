using Optimization;
using UnityEngine;

namespace Dream
{
    public class BabyPool : MonoBehaviour
    {
        [SerializeField] DreamSceneConfig _config;
        public PoolMono<Baby> Pool { get; private set; }

        //Initialize baby pool
        private void Start()
        {
            GameObject b = GetBaby.Instance.GetChoosenBaby();

            Pool = new PoolMono<Baby>(b.GetComponent<Baby>(), 1, true, true, transform);
        }
    }
}