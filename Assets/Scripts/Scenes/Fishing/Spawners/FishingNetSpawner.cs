using Sound;
using System;
using UnityEngine;

namespace Fishing.Spawners
{
    public class FishingNetSpawner : MonoBehaviour
    {
        public event Action<FishingNet> OnSpawn;

        [SerializeField] private FishingNetPool _pool;
        [SerializeField] private Transform _netSpawnPosition;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private Camera _camera;
        
        public void SpawnNet()
        {
            var destination = _camera.ScreenToWorldPoint(new Vector3((4.7f * Screen.width) / 5, (1 * Screen.height) / 2, 1));
            FishingNet net = _pool.Pool.GetFreeElement();
            net.transform.position = _netSpawnPosition.position;
            net.Construct(destination, _soundSystem);
            OnSpawn?.Invoke(net);
        }
    }
}