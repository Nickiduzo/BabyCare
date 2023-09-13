using Inputs;
using Sound;
using System;
using UnityEngine;

namespace Feeding { 
    public class NapkinSpawn : MonoBehaviour
    {
        public event Action<Napkin> OnSpawn;

        [SerializeField] private NapkinPool _pool;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private SoundSystem _soundSystem;


        //Spawn Napkin
        public Napkin SpawnNapkin(Vector3 at)
        {
            Napkin napkin = _pool.PoolNapkin.GetFreeElement();
            napkin.transform.position = at;
            napkin.Construct(_inputSystem, _soundSystem);
            OnSpawn?.Invoke(napkin);
            return napkin;
        }
    }
}
