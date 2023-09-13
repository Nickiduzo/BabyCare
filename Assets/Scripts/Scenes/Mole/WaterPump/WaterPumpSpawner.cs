using Inputs;
using Sound;
using UnityEngine;

namespace Mole.Spawners
{
    public class WaterPumpSpawner : MonoBehaviour
    {
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private WaterPumpPool _pool;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _destinationPoint;

        // get [WaterPump] from pool, set stawn point for it
        public WaterPump SpawnWaterPump()
        {
            WaterPump pump = _pool.Pool.GetFreeElement();
            pump.transform.position = _spawnPoint.position;
            pump.Construct(_destinationPoint.position, _spawnPoint.position, _inputSystem);
            pump.GetComponentInChildren<WaterPumpStreamPrefab>().Construct(_soundSystem);
            return pump;
        }
    }
}