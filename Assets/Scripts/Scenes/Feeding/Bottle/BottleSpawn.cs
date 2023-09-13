using Inputs;
using System;
using UnityEngine;


namespace Feeding { 
    public class BottleSpawn : MonoBehaviour
    {
        public event Action<Bottle> OnSpawn;

        [SerializeField] private BottlePool_ _pool;
        [SerializeField] private InputSystem _inputSystem;

        //Spawn bottle
        public Bottle SpawnBottle(Vector3 at)
        {
            Bottle bottle = _pool.Pool.GetFreeElement();
            bottle.transform.position = at;
            bottle.Construct(_inputSystem);
            OnSpawn?.Invoke(bottle);
            return bottle;
        }
    }
}
