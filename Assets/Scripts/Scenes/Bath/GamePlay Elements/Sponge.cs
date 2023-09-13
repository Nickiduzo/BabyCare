using Inputs;
using Sound;
using System;
using UnityEngine;

namespace Bath
{
    public class Sponge : MonoBehaviour
    {
        public event Action OnDrugStart;
        public event Action OnDrugEnded;
        public event Action OnBubbleBaby;

        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] private BabyTriggerObserver _babyObserver;
        [SerializeField] private BubleTriggerObserver _bubleObserver;
        [SerializeField] private DragAndDrop _dragAndDrop;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private string _sound;

        private InputSystem _input;
        private SoundSystem _soundSystem;
        private BubbleSpawner _bubbleSpawner;

        private Vector3 _spawnPoint;
        private bool _isInBuble;
        //Main constructor for pool
        public void Construct(SoundSystem sound, InputSystem input, Vector3 spawnPoint, BubbleSpawner bubbleSpawner)
        {
            _input = input;
            _soundSystem = sound;
            _spawnPoint = spawnPoint;
            _bubbleSpawner = bubbleSpawner;

            _dragAndDrop.Construct(_input);
            _dragAndDrop.OnDragStart += UpSortingOrder;
            _dragAndDrop.OnDragStart += () => OnDrugStart?.Invoke();
            _dragAndDrop.OnDragEnded += () => OnDrugEnded?.Invoke();
            _dragAndDrop.OnDragEnded += DownSortingOrder;

            _destinationOnDragEnd.Construct(_spawnPoint);
            _babyObserver.OnTriggerStay += BubbleBaby;

            _bubleObserver.OnTriggerEnter += CheckIfInBubble;
            _bubleObserver.OnTriggerStay += CheckIfInBubble;
            _bubleObserver.OnTriggerExit += CheckIfExitBubble;
        }

        //Sets sorting order to 13
        private void UpSortingOrder()
        {
            _spriteRenderer.sortingOrder = 13;
        }

        //Sets sorting order to 10
        private void DownSortingOrder()
        {
            _spriteRenderer.sortingOrder = 10;
        }

        //Spawns bubbles on Baby object
        private void BubbleBaby(Baby baby)
        {
            if (!_isInBuble)
            {
                _bubbleSpawner.SpawnBubble(transform);
                _soundSystem.PlaySound(_sound);
                OnBubbleBaby?.Invoke();
            }
        }

        //Sets bool _isInBubble to true, if sponge is triggering bubble object, so it couldn't spawn another bubble inside current
        private void CheckIfInBubble(Bubble bubble)
        {
            _isInBuble = true;
        }

        //Sets bool _isInBubble to false, if sponge is exiting trigger of bubble object, so it could spawn another bubble
        private void CheckIfExitBubble(Bubble bubble)
        {
            _isInBuble = false;
        }
    }
}