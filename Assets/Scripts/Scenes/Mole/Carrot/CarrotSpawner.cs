using Inputs;
using System;
using UnityEngine;
using UsefulComponents;

namespace Mole.Spawners
{
    public class CarrotSpawner : MonoBehaviour
    {
        public event Action<Carrot> OnSpawn;

        [SerializeField] private CarrotPool _pool;
        [SerializeField] private InputSystem _inputSystem;

        // get carrot from pool and invoke Action [OnSpawn] in which we pass "Carrot"
        public Carrot SpawnCarrot(Vector3 at)
        {
            HintSystem.Instance.HidePointerHint();
            Carrot carrot = _pool.Pool.GetFreeElement();
            carrot.transform.position = at;
            carrot.Construct(_inputSystem);
            OnSpawn?.Invoke(carrot);
            return carrot;
        }
    }
}