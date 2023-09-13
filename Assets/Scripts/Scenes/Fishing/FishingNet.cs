using DG.Tweening;
using Sound;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fishing
{
    public class FishingNet : MonoBehaviour
    {
        public event Action OnCatchEnough;
        public Vector3 NetStorePosition => CalculatePositionInNet();

        [SerializeField] private FishLevelConfig _config;
        [SerializeField] private Transform _fishStorePosition;
        [SerializeField] private BoxCollider2D _netCollider;
        [SerializeField] private float _fishStoreStep;
        [SerializeField] private float _xOffSetMultiplier;
        [SerializeField] private int _fishCountToRandomSpawn;

        private float _fishStoreOffset;
        public int FishCount => _caughtFishes.Count;
        private List<Fish> _caughtFishes = new();
        private Vector3 _cachedScale;
        private Vector3 _spawnPosition;

        private Vector3 Destination { get; set; }

        public SoundSystem SoundSystem { get; private set; }


        public void Construct(Vector3 destination, SoundSystem soundSystem)
        {
            _cachedScale = transform.localScale;
            _spawnPosition = transform.position;
            transform.localScale = Vector3.zero;
            Destination = destination;
            SoundSystem = soundSystem;
            Appear();
        }

        public void Disappear()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(1);
            sequence.Append(transform.DOMove(_spawnPosition, 0.5f));
        }
        
        private void Appear()
        {
            transform.DOMove(Destination, 0.5f);
            transform.DOScale(_cachedScale, 0.1f);
        }

        private void OnTriggerEnter2D(Collider2D target)
        {
            if (TryGetFish(target, out Fish fish))
            {
                if (!fish.IsCatch || fish.IsStored) return;

                fish.Stored();
                _caughtFishes.Add(fish);

                MakeChildOfNet(fish);
                NextStep();

                if (!IsCatchEnough()) return;

                OnCatchEnough?.Invoke();
            }
        }

        private void MakeChildOfNet(Fish fish)
            => fish.transform.SetParent(transform);

        private void NextStep()
        {
            _fishStoreOffset += _fishStoreStep;

            if (IsRandomSpawnReady())
                _xOffSetMultiplier *= Random.Range(-1, 1);
            else
                _xOffSetMultiplier *= -1;
        }

        private bool IsRandomSpawnReady()
            => FishCount > _fishCountToRandomSpawn;

        private Vector3 CalculatePositionInNet()
        {
            Vector3 offSet = CalculateFishPositionOffSet();
            return _fishStorePosition.position + offSet;
        }
        
        private Vector2 CalculateFishPositionOffSet()
            => new(_fishStoreOffset * _xOffSetMultiplier, _fishStoreOffset);

        public void MakeCaughtFishesDraggable()
        {
            _netCollider.enabled = false;

            foreach (Fish caughtFish in _caughtFishes)
                caughtFish.GetComponent<DragAndDrop>().IsDraggable = true;
        }

        private bool TryGetFish(Component target, out Fish fish)
            => target.transform.TryGetComponent(out fish);

        private bool IsCatchEnough()
            => FishCount == _config.FishSpawnCount;
    }
}