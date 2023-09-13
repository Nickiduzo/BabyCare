using System.Collections.Generic;
using DG.Tweening;
using Fishing.Spawners;
using Inputs;
using UnityEngine;

namespace Fishing
{
    public class Fish : MonoBehaviour, IBasketCollectable
    {
        private readonly int Catch = Animator.StringToHash("Catch");
        public bool IsCatch => _isCatch;
        public bool IsStored => _isStored;
        public bool IsStoredInBasket => _isStoredInBasket;
        public bool IsCollected { get; private set; }
        
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _jumpPower;
        [SerializeField] private RandomMover _randomMover;
        [SerializeField] private RandomSprite _randomSprite;
        [SerializeField] private DragAndDrop _dragAndDrop;
        [SerializeField] private float _movingDuration;
        [SerializeField] private int _rotateAngle = 359;

        private bool _isCatch;
        private bool _isStored;
        private bool _isStoredInBasket;
        
        private Vector3 _positionInNet;
        private Animator Animator { get; set; }
        public List<SpriteRenderer> Renderers { get; private set; }

        private void Awake()
        {
            SetUpFishSprite();
            _dragAndDrop.IsDraggable = false;
            _dragAndDrop.OnDragEnded += MoveToNet;
        }

        private void SetUpFishSprite()
        {
            var fishVariant = _randomSprite.GetRandomSprite();
            var body = Instantiate(fishVariant, this.transform.position, Quaternion.identity, this.transform);
            Animator = body.Animator;
            Renderers = body.SpriteRenderers;
        }

        public void Construct(InputSystem inputSystem, FishSpawner fishSpawner)
        {
            _dragAndDrop.Construct(inputSystem);
            fishSpawner.OnFishesDisappear += MoveFromScreen;
            _randomMover.Construct(Animator);
        }

        public void ChangeSpriteSortOrder(int SortOrderIndex)
        {
            _randomSprite.SetUpSpriteOrder(SortOrderIndex);
        }

        private void MoveFromScreen()
        {
            if (!IsStored)
            {
                _collider.enabled = false;
                gameObject.SetActive(false);
            }
            _randomMover.StopOnReachingDestination();
        }

        public void SetPositionInNet(Vector3 position)
            => _positionInNet = position;

        private void MoveToNet()
        {
            if (IsCollected) return;

            transform.DOMove(_positionInNet, _movingDuration);
        }

        public void Hooked()
        {
            _randomMover.enabled = false;
            Animator.SetTrigger(Catch);
            _isCatch = true;
        }
        
        private void MakeNonInteractable()
        {
            _collider.enabled = false;
            _dragAndDrop.IsDraggable = false;
        }

        public void StoredToBusket()
        {
            _isStoredInBasket = true;
            MakeNonInteractable();
        }
        
        public void Stored()
        {
            _isStored = true;
            transform.SetParent(null);
        }

        public Tween JumpTo(Vector3 position, bool isRotate = true)
        {
            if (isRotate)
                transform.DOLocalRotate(CalculateRotation(), 1f, RotateMode.LocalAxisAdd);

            return transform.DOJump(position, _jumpPower, 1, 1f);
        }

        private Vector3 CalculateRotation() => new(0, 0, _rotateAngle);
/*
        public Tween MoveTo(Vector3 point)
            => transform.DOMove(point, _movingDuration);
            */

        public void MakeVisualPartOf(SpriteRenderer _frontBasketSprite, int _sortingIndex, int _sortingLayer)
        {
            foreach (var sprite in Renderers)
            {
                sprite.sortingOrder = _sortingIndex;
                sprite.sortingLayerID = _sortingLayer;
            }
        }
    }
}