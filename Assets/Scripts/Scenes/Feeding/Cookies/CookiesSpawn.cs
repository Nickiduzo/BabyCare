using Inputs;
using System;
using UnityEngine;


namespace Feeding
{
    public class CookiesSpawn : MonoBehaviour
    {
       
        public event Action<Cookies> OnSpawn;

        [SerializeField] private CookiesPool _pool;
        [SerializeField] private InputSystem _inputSystem;


        //Spawn Cookies
        public Cookies SpawnCookies(Vector3 at , Quaternion rotation)
        {
            Cookies cookies = _pool.PoolCookies.GetFreeElement();
            cookies.transform.position = at;
            cookies.transform.rotation = rotation;
            cookies.Construct(_inputSystem);
            OnSpawn?.Invoke(cookies);
            return cookies;
        }
    }
}
