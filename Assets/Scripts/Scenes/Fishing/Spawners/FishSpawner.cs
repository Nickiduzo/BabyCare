using Inputs;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fishing.Spawners
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private FishLevelConfig _config;
        [SerializeField] private float _minFishSpawnY;
        [SerializeField] private float _maxFishSpawnY;
        [SerializeField] private float _bounceSpawnOffSetX;
        [SerializeField] private int _extraFishes;

        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _fishesContainerRight;
        [SerializeField] private Transform _fishesContainerLeft;

        [SerializeField] private Transform _environmentContainer;

        public event Action OnFishesSpawned;
        public event Action OnFishesDisappear;
        private Vector3 LowerRightCorner;
        private Vector3 UpperLeftCorner;
        private int _countToSpawn => _config.FishSpawnCount;
        private int _sortOrderIndex = 1;

        private void Awake()
        {
            LowerRightCorner = _camera.ScreenToWorldPoint(new Vector3(0.7f * Screen.width, 0.15f * Screen.height, 1));
            UpperLeftCorner = _camera.ScreenToWorldPoint(new Vector3(1f * Screen.width, 0.45f * Screen.height, 1));
            SpawnFishesLeftSide();
            SpawnFishesRightSide();
        
        }

        public void DisappearFishes() => OnFishesDisappear?.Invoke();

        private void SpawnFishesRightSide()
        {
            for (int i = 0; i < _countToSpawn + _extraFishes; i++)
            {
                Vector2 range = GetPositionRight();
                Fish fish = Instantiate(_config.Fish, range, _config.Fish.transform.rotation, _fishesContainerRight);
                fish.Construct(_inputSystem, this);
            }
            OnFishesSpawned?.Invoke();
        }

        private void SpawnFishesLeftSide()
        {
            for (int i = 0; i < _countToSpawn + _extraFishes; i++)
            {
                Vector2 range = GetPositionLeft();
                Fish fish = Instantiate(_config.Fish, range, _config.Fish.transform.rotation, _fishesContainerLeft);
                fish.Construct(_inputSystem, this);
                fish.ChangeSpriteSortOrder(_sortOrderIndex);
            }
            OnFishesSpawned?.Invoke();
        }

        private Vector2 GetPositionRight()
        {
            float spawnX = Camera.main.ViewportToWorldPoint(new Vector3(1f, Random.Range(0.2f, 0.8f), 0f)).x + GetRandomOffset();
            float spawnY = GetRandomPositionY();
            return new Vector2(spawnX, spawnY);
        }

        private Vector2 GetPositionLeft()
        {
            float spawnX = Camera.main.ViewportToWorldPoint(new Vector3(0f, Random.Range(0.2f, 0.8f), 0f)).x - GetRandomOffset();
            float spawnY = GetRandomPositionY();
            return new Vector2(spawnX, spawnY);
        }

        private float GetRandomOffset()
        {
            float minOffset = -_bounceSpawnOffSetX;
            float maxOffset = _bounceSpawnOffSetX;
            return Random.Range(minOffset, maxOffset);
        }

        private float GetRandomPositionY()
        => UnityEngine.Random.Range(LowerRightCorner.y, UpperLeftCorner.y);
    }
}
