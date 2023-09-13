using Sound;
using UnityEngine;

namespace Dream
{
    public class ToyWheelSpawner : MonoBehaviour
    {
        [SerializeField] private ToyWheelPool _pool;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private Transform _spawnPoint;

        //Spawn and construct ToyWheel
        public ToyWheel SpawnToyWheel()
        {
            ToyWheel toyWheel = _pool.Pool.GetFreeElement();
            toyWheel.transform.position = _spawnPoint.position;
            toyWheel.Construct(_soundSystem);
            return toyWheel;
        }
    }
}
