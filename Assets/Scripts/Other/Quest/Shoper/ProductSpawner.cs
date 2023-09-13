using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Scene;

namespace Quest
{
    public class ProductSpawner : MonoBehaviour
    {
        [SerializeField] private Product[] _products;
        [SerializeField] private GameObject _basket;
        [SerializeField] private Ease _movingEase;
        [SerializeField] private float _duration;
        [SerializeField] private float _spawnDelayRange;        
        [SerializeField] private float _apearHight;
        [SerializeField] private float _jumpPower;

        private float _pointOffSetX = 0.3f;
        private float _offSetX = 0.3f;
        private float _offSetY = 0.4f;
        private int _numberProductsOnDestination;
        private int _selectedProduct;
        private int _selectedProductQuantity;
        private Vector3 _basketPoint;
        private Vector3 _basketDestination;

        private Dictionary<SceneType, System.Action> _sceneCodeDictionary;

        
        private void Awake()
        {
            _sceneCodeDictionary = new Dictionary<SceneType, System.Action>()
            {
                { SceneType.Fishing, SelectedFish },
                { SceneType.Carrot, SelectedCarrot },
                { SceneType.Apple, SelectedApple },
                { SceneType.Bee, SelectedBee },
                { SceneType.Chicken, SelectedChicken },
                { SceneType.Cow, SelectedCow },
                { SceneType.Sheep, SelectedSheep },
                { SceneType.Tomato, SelectedTomato },
                { SceneType.Sunflower, SelectedSunflower},
            };
        }

        // choose right product depending on type of scene
        public void ProductSelection(SceneType _config, Transform _basketSpawnPoint, Transform _basketDestinationPoint)
        {
            
            if (_sceneCodeDictionary.ContainsKey(_config))
            {
                _sceneCodeDictionary[_config]?.Invoke();
                if (SceneLoader.IsLevelComplied)
                {
                    SpawnBasket(_basketSpawnPoint, _basketDestinationPoint);
                }
            }
            else
            {
                Debug.LogError("No code defined for SceneType: " + _config);
            }
        }

        // create basket and set up necessary parameters
        private void SpawnBasket(Transform _basketSpawnPoint, Transform _basketDestinationPoint)
        {
            _basketDestination = _basketDestinationPoint.position;
            _basketPoint = _basketSpawnPoint.position;
            _basketPoint.y += _offSetY;
            _basket.transform.position = _basketSpawnPoint.position;
            _basket.transform.DOScale(Vector3.one, 1f)
                .OnComplete(() =>
                {                    
                    SpawnProduct();                    
                });
        }

        // create products and move to basket
        private void SpawnProduct()
        {
            _basketPoint.x -= _pointOffSetX * 3f;
            if (_selectedProduct >= 0 && _selectedProduct < _products.Length)
            {
                var selectedProduct = _products[_selectedProduct];
                _basketPoint.x += _offSetX;

                for (int i = 0; i < _selectedProductQuantity; i++)
                {
                    Vector3 spawnPosition = _basketPoint + new Vector3(i * _offSetX, 0f, 0f);
                    Product spawnedProduct = Instantiate(selectedProduct, spawnPosition, Quaternion.identity, _basket.transform);
                    spawnedProduct.MoveToBasket(Random.Range(0f, _spawnDelayRange), _apearHight, _duration, _movingEase, _basket)
                        .OnComplete(() =>
                        {
                            AllProductsStored();
                        });
                }
            }
        }

        // check if all products are stored
        private void AllProductsStored()
        {
            _numberProductsOnDestination++;
            if(_numberProductsOnDestination == _selectedProductQuantity)
                MoveBasketToCar();
        }

        // move basket with products to car
        private void MoveBasketToCar()
            => _basket.transform.DOJump(_basketDestination, _jumpPower, 1, 1);

        // set special parameters and values for different product
        private void SelectedFish() { _selectedProduct = 0; _selectedProductQuantity = 4; _offSetX = 0.4f; }
        private void SelectedCarrot() { _selectedProduct = 1; _selectedProductQuantity = 5; }
        private void SelectedApple() { _selectedProduct = 2; _selectedProductQuantity = 5; }
        private void SelectedBee() { _selectedProduct = 3; _selectedProductQuantity = 3; _offSetX = 0.46f; }
        private void SelectedChicken() { _selectedProduct = 4; _selectedProductQuantity = 5; }
        private void SelectedCow() { _selectedProduct = 5; _selectedProductQuantity = 3;  _offSetX = 0.45f; }
        private void SelectedSheep() { _selectedProduct = 6; _selectedProductQuantity = 3; _offSetX = 0.4f; _pointOffSetX = 0.28f; }
        private void SelectedTomato() { _selectedProduct = 7; _selectedProductQuantity = 5; }
        private void SelectedSunflower() { _selectedProduct = 8; _selectedProductQuantity = 5; }
    }
}

