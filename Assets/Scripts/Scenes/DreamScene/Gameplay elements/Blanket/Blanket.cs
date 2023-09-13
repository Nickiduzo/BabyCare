using System;
using DG.Tweening;
using Inputs;
using Sound;
using UnityEngine;
using UsefulComponents;

namespace Dream
{
    public class Blanket : MonoBehaviour
    {
        public event Action OnCoveredBaby;

        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] private BabyTriggerObserver _observer;
        [SerializeField] private DragAndDrop _dragAndDrop;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private InputSystem _input;
        private SoundSystem _soundSystem;
        private Transform _destination;

        private Vector3 _spawnPoint;

        private bool _isTriggerBaby;

        public void Construct(InputSystem input, SoundSystem soundSystem, Transform destination, Vector3 spawnPoint)
        {
            _input = input;
            _soundSystem = soundSystem;
            _destination = destination;
            _spawnPoint = spawnPoint;

            _dragAndDrop.Construct(_input);
            _dragAndDrop.OnDragStart += DisableHint;
            _dragAndDrop.OnDragEnded += PutBlanketOnBed;
            _destinationOnDragEnd.Construct(_spawnPoint);

            _observer.OnTriggerEnter += CheckIfTriggerBaby;
            _observer.OnTriggerExit += CheckIfExitBaby;

        }

        private void Start()
        {
            ActivateHint();
        }

        //Checking if blanket in baby trigger
        private void CheckIfTriggerBaby(Baby baby)
        {
            _isTriggerBaby = true;
            _dragAndDrop.OnDragEnded -= _destinationOnDragEnd.MoveToDestination;
        }

        //Checking if blanket not in baby trigger
        private void CheckIfExitBaby(Baby baby)
        {
            _isTriggerBaby = false;
            _dragAndDrop.OnDragEnded += _destinationOnDragEnd.MoveToDestination;
        }

        //Put blanket on the baby. Invoking when blanket dragged on baby.
        //TODO extract fields from magic values
        private void PutBlanketOnBed()
        {
            if (_isTriggerBaby)
            {
                _soundSystem.PlaySound("Blanket");
                transform.DOMove(_destination.position, 0.5f);
                transform.DOScale(0.9f, 0.5f);

                _spriteRenderer.sortingOrder = 8;
                transform.position = _destination.position;

                OnCoveredBaby?.Invoke();
                _dragAndDrop.IsDraggable = false;
            }
        }

        private void ActivateHint()
            => HintSystem.Instance.ShowPointerHint(_spawnPoint, _destination.position, 0.5f);

        private void DisableHint()
            => HintSystem.Instance.HidePointerHint();
    }
}
