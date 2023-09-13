using Inputs;
using Sound;
using UnityEngine;

namespace Bath
{
    public class SpongeSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private SoundSystem _soundSystem;

        [SerializeField] private SpongePool _pool;
        //Initialize sponge
        public Sponge SpawnSponge(BubbleSpawner bubbleSpawner)
        {
            Sponge sponge = _pool.Pool.GetFreeElement();
            sponge.transform.position = _spawnPoint.position;
            sponge.Construct(_soundSystem, _inputSystem, _spawnPoint.position, bubbleSpawner);
            return sponge;
        }
    }
}
