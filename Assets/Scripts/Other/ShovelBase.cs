using DG.Tweening;
using Sound;
using Inputs;
using UnityEngine;
using UsefulComponents;


    public class ShovelBase : MonoBehaviour
    {
        [SerializeField] protected Collider2D _collider;
        [SerializeField] protected MoveStartDestination _move;
        [SerializeField] protected MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] protected DragAndDrop _dragAndDrop;
        [SerializeField] protected Animator _animator;

        protected bool _isDigging;

        public void Construct(SoundSystem soundSystem, InputSystem inputSystem, Vector3 destinationPoint, Vector3 spawnPoint)
        {
            _dragAndDrop.Construct(inputSystem);
            _destinationOnDragEnd.Construct(destinationPoint);
            _move.Construct(destinationPoint, spawnPoint);
            Awake();
        }

        protected virtual void Awake()
        {
            _dragAndDrop.OnDragStart += DeactivateHint;
            _dragAndDrop.OnDragEnded += MoveToBaseDestination;
        }

        protected virtual void OnDestroy()
        {
            _dragAndDrop.OnDragStart -= DeactivateHint;
            _dragAndDrop.OnDragEnded -= MoveToBaseDestination;
        }

        protected virtual void MakeNonInteractable()
        {
            DOTween.Kill(gameObject);
            _dragAndDrop.IsDraggable = false;
            _collider.enabled = false;
        }

        private void MoveToBaseDestination()
            => _destinationOnDragEnd.MoveToDestination();
        
        protected virtual Tween MoveToStartPoint()
            => _move.MoveToStart();

        protected virtual void MoveToDestination()
            => _move.MoveToDestination();

        protected virtual void SetDig(bool value)
        {
            if (IsAnimatorAvailable())
            {
                _animator.SetBool("State", value);
            }
        }
        
        protected virtual void DeactivateHint()
        {
            HintSystem.Instance.HidePointerHint();
        }

        protected virtual bool IsAnimatorAvailable()
        {
            return _animator != null;
        }

        protected virtual void ShowFx(Transform fxSpawnPoint)
            => FxSystem.Instance.PlayEffect(0, fxSpawnPoint.position);
        
    }
