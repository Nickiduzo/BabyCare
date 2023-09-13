using DG.Tweening;
using System;
using UnityEngine;

namespace Quest
{
    public class Shopper : MonoBehaviour
    {
        [SerializeField] private float _movingDuration;
        [SerializeField] private GameObject[] _skins;
        [SerializeField] private Transform _skinContainer;
        [SerializeField] private ProductSpawner _productSpawner;
        [SerializeField] private AnimalSelector _animalSelector;

        private TaskSpawner _taskSpawner;
        private Task _currentTask;
        public event Action OnArrived;
        public int SkinsCount => _skins.Length;

        public void Construct(TaskSpawner taskSpawner, int skinId)
        {
            _taskSpawner = taskSpawner;
            SetSkin(skinId);
        }

        // move shopper to destination and spawn random task
        public void MoveToAndSpawn(Vector3 target)
            => MoveTo(target)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _currentTask = _taskSpawner.SpawnRandomTask();
                    SetTaskChildOfShopper(_currentTask, this);
                    OnArrived?.Invoke();
                });

        // move shopper to scene and destroys task if it is available
        public Tween MoveTo(Vector3 target)
        {
            if (_currentTask != null)
                Destroy(_currentTask.gameObject);
            return transform.DOMove(target, _movingDuration).SetEase(Ease.InBack);
        }

        // get type of product from config
        public void SetProduct(SceneType _config, Transform _basketSpawnPoint, Transform _basketDestinationPoint)
        {
            _productSpawner.ProductSelection(_config, _basketSpawnPoint, _basketDestinationPoint);
            Debug.Log("product is active " + _config);
        }

        // create new random animal and set in config
        public void SetRandomAnimal(QuestLevelConfig _config)
            => _animalSelector.SetRandomAnimal(_config);
        
        // take available animal from config
        public void GetAnimal(QuestLevelConfig _config)
            => _animalSelector.GetAnimalFromConfig(_config);
        
        // create car skin in skin container
        public void SetSkin(int skinId)
            => Instantiate(_skins[skinId], _skinContainer);
        
        // make task child object of shopper
        private void SetTaskChildOfShopper(Task task, Shopper shopper)
            => task.transform.SetParent(shopper.transform);
    }
}