using Sound;
using UnityEngine;

namespace Dream
{
    public class LampSpawner : MonoBehaviour
    {
        [SerializeField] private LampPool _pool;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private Transform _spawnPoint;

        //Spawn and construct lamp
        public Lamp SpawnLamp()
        {
            Lamp lamp = _pool.Pool.GetFreeElement();
            lamp.transform.position = _spawnPoint.position;
            lamp.Construct(_soundSystem);
            return lamp;
        }
    }
}
