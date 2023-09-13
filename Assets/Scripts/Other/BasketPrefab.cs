using DG.Tweening;
using Sound;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UsefulComponents;

    public class BasketPrefab : MonoBehaviour, IWinInvoker
    {
        private const string SUCCESS_CONST = "Success";

        public event Action OnWin;
        public event Action OnBasketArrived;
        public event Action OnBasketSpawned;
        public event Action OnBasketLeft;
        public event Action OnAllProductsStored;

        public int STORED_MAX_COUNT;

        [SerializeField] private Transform _topBasketPositition;
        [SerializeField] private float _movingDuration;
        [SerializeField] private List<Transform> _storePoints;
        [SerializeField] private SpriteRenderer _basketFrontSprite;
        [SerializeField] private SpriteRenderer _basketBackSprite;
        [SerializeField] private Animator _collectAnimation;
        [SerializeField] private string _animationTriggerName;

        private Vector3 _spawnPoint;
        private Vector3 _destination;
        private int _storedCount;
        private int _step;
        private int _sortingIndex;
        private int _sortingLayer;
        private float _delay;
        private bool _invokeWinPanel;
        private FxSystem _fxSystem;
        private SoundSystem _soundSystem;
        private ArrowController _arrowController;

        public void Construct(Vector3 spawnPoint, Vector3 destination, FxSystem fxSystem, SoundSystem soundSystem, ArrowController arrowController, float delay)
        {
            _delay = delay;
            _spawnPoint = spawnPoint;
            _destination = destination;
            _fxSystem = fxSystem;
            _soundSystem = soundSystem;
            _invokeWinPanel = true;
            _arrowController = arrowController;
            OnBasketSpawned?.Invoke();
            _sortingLayer = _basketBackSprite.sortingLayerID;

            SetStorePoints();
            transform.position = _spawnPoint;
            MoveToDestinationPoint().OnComplete(OnDestinationArrive);
        }

        public int SetSortingIndex()
            => _sortingIndex = _basketFrontSprite.sortingOrder - STORED_MAX_COUNT - 2;
        
        public bool CanInvokeOnWin(bool _canInvokeOnWin)
            => _invokeWinPanel = _canInvokeOnWin;

        private void SetStorePoints()
        {
            GameObject basketStorePointsObj = GameObject.Find("BasketStorePoints");
            int childCount = basketStorePointsObj.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                _storePoints.Add(basketStorePointsObj.transform.GetChild(i));
            }
        }

        private void OnDestinationArrive()
        {
            OnBasketArrived?.Invoke();
            _arrowController.ShowArrow();
            DOTween.Sequence().Append(_arrowController.MoveAheadArrow()).SetLoops(-1, LoopType.Yoyo);
        }

        public void StoreObj<T>(T item) where T : MonoBehaviour, IBasketCollectable
        {
            item.GetComponent<DragAndDrop>().IsDraggable = false;
            item.GetComponent<Collider2D>().enabled = false;
            _arrowController.HideArrow();
            HideHint();

            DOVirtual.DelayedCall(_movingDuration / 2, () =>
            {
                item.MakeVisualPartOf(_basketFrontSprite, _sortingIndex, _sortingLayer);
                _sortingIndex++;
            });
            
            
            var sequence = DOTween.Sequence();
            sequence.Append(item.transform.DOMove(_topBasketPositition.position, _movingDuration / 2)
                .OnStepComplete(() => {if (_collectAnimation != null) _collectAnimation.SetTrigger(_animationTriggerName);}));
            sequence.Append(item.transform.DOMove(CalculatePointInBasket(item), _movingDuration)
                .OnComplete(() => {StoreItem(item);}));

            NextStep();
        }
        
        private void NextStep()
        {
            _step++;

            if (MaxStep())
                _step = 0;
        }

        private bool MaxStep()
            => _step == STORED_MAX_COUNT;

        private Vector3 CalculatePointInBasket<T>(T item) where T : MonoBehaviour
        {
            item.transform.rotation = _storePoints[_step].rotation;
            return _storePoints[_step].position;
        }

        private void StoreItem<T>(T item) where T : MonoBehaviour
        {
            item.transform.SetParent(transform);
            _storedCount++;
            print($"{item} ::: {_storedCount}");

            _fxSystem.PlayEffect(SUCCESS_CONST, item.transform.position);
            _soundSystem.PlaySound(SUCCESS_CONST);

            if (BasketIsFull())
            {
                OnAllProductsStored?.Invoke();
                DOVirtual.DelayedCall(_delay, () =>
                {
                    MoveToSpawnPoint().OnComplete(() =>
                    {
                        if(_invokeWinPanel)
                            OnWin?.Invoke();
                        else
                            Destroy(gameObject);
                    });
                });
            }
        }

        private bool BasketIsFull()
            => _storedCount >= STORED_MAX_COUNT;

        private Tween MoveToDestinationPoint() => transform.DOMove(_destination, _movingDuration);

        private Tween MoveToSpawnPoint()
            => transform.DOMove(_spawnPoint, _movingDuration).OnComplete(() => OnBasketLeft?.Invoke());
        private void HideHint()
        {
            HintSystem.Instance.HidePointerHint();
        }
    }