using Optimization;
using UnityEngine;

namespace Bath
{
    public class BabyPool : MonoBehaviour
    {
        public PoolMono<Baby> Pool { get; private set; }

        private void Awake()
        {
            GameObject b = GetBaby.Instance.GetChoosenBaby();
            Pool = new PoolMono<Baby>(b.GetComponent<Baby>(), 1, true, true, transform);
        }
    }
}
