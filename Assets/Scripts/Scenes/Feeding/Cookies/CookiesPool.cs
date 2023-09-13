using Optimization;
using UnityEngine;


namespace Feeding
{
    public class CookiesPool : MonoBehaviour
    {
        [SerializeField] private FeedingSceneController _config;
        public PoolMono<Cookies> PoolCookies { get; private set; }

        // get Cookies from config for pool, set number of cookies that will appear
        private void Awake()
            => PoolCookies = new PoolMono<Cookies>(_config.cookie, 3, true, true, transform);
    }
}
