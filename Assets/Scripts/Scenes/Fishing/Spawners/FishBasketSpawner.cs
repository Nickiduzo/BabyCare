using Sound;
using System;
using UnityEngine;

namespace Fishing.Spawners
{
    public class FishBasketSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _basketSpawnPosition;
        [SerializeField] private Transform _basketDestinationPoint;
        [SerializeField] private FishingBasketPool _fishingNetPool;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private FxSystem _fxSystem;
        [SerializeField] private ArrowController _arrowController;
        [SerializeField] private float _delay;

        public BasketPrefab SpawnFishBasket()
        {
            BasketPrefab basket = _fishingNetPool.Pool.GetFreeElement();
            basket.Construct(_basketSpawnPosition.position, _basketDestinationPoint.position, _fxSystem, _soundSystem,
                _arrowController, _delay);

            return basket;
        }
    }
}