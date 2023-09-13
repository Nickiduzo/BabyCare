using Optimization;
using UnityEngine;

namespace QuestBaby
{
    public class BabyPool : MonoBehaviour
    {
        public PoolMono<Baby> PoolBaby { get; private set; }

        // get Baby from config for pool, set number of babys that will appear
        private void Awake()
        {
            GameObject b = GetBaby.Instance.GetChoosenBaby();
            PoolBaby = new PoolMono<Baby>(b.GetComponent<Baby>(), 1, true, true, transform);
        }
    }
}