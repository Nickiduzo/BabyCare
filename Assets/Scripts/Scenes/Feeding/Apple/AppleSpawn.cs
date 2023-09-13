using Inputs;
using System;
using UnityEngine;


namespace Feeding
{
    public class AppleSpawn : MonoBehaviour
    {
        public event Action<Apple> OnSpawn;

        [SerializeField] private ApplePool _pool;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] public GameObject spawnApplePoint;
        // Spawn Apple
        public Apple SpawnApple()
        {
            Apple apple = _pool.PoolApple.GetFreeElement();
            apple.transform.position = spawnApplePoint.transform.position;
            apple.Construct(_inputSystem);
            OnSpawn?.Invoke(apple);
            return apple;
        }
    }
}
