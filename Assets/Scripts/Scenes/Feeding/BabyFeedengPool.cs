using Optimization;
using UnityEngine;

namespace Feeding
{
    public class BabyFeedengPool : MonoBehaviour
    {

        [SerializeField] private FeedingSceneController _config;
        
        public PoolMono<BabyFeeding> PoolBaby { get; private set; }

        // get Baby from config for pool, set number of babys that will appear
        private void Awake()
        {
           GameObject baby = GetBaby.Instance.GetChoosenBaby();        
           PoolBaby = new PoolMono<BabyFeeding>(baby.GetComponent<BabyFeeding>(), 1, true, true, transform);
        }
          

    }
}
