using Sound;
using Unity.VisualScripting;
using UnityEngine;

namespace Dream
{
    public class BabySpawner : MonoBehaviour
    {
        [SerializeField] private BabyPool _pool;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private Transform _spawnPoint;

        //Spawn and construct Baby
        //Return: spawned Baby[baby].
        public Baby SpawnBaby()
        {
            Baby baby = _pool.Pool.GetFreeElement().GetComponent<Baby>();

            baby.transform.position = _spawnPoint.position;
            baby.Construct(_soundSystem);

            return baby;
        }
    }
}