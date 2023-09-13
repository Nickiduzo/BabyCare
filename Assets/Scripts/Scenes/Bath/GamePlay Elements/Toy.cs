using Inputs;
using Sound;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Bath
{
    public class Toy : MonoBehaviour
    {
        public event Action OnGiveBabyAToy;

        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] private BabyTriggerObserver _observer;
        [SerializeField] private DragAndDrop _dragAndDrop;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        //Unserizalize when config is done
        private InputSystem _input;
        private SoundSystem _sound;

        private Vector3 _spawnPoint;
        private bool _triggerBaby = false;
        //Main constructor for pool
        public void Construct(InputSystem input, SoundSystem soundSystem, Vector3 spawnPoint)
        {
            _input = input;
            _sound = soundSystem;
            _spawnPoint = spawnPoint;

            _dragAndDrop.Construct(_input);
            _dragAndDrop.OnDragEnded += GiveBabyAToy;
            _dragAndDrop.OnDragStart += UpSortingOrder;
            _dragAndDrop.OnDragEnded += DownSortingOrder;

            _destinationOnDragEnd.Construct(_spawnPoint);

            _observer.OnTriggerEnter += CheckIfTriggerBaby;
            _observer.OnTriggerExit += CheckIfExitBaby;
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

        //Sets bool _triggerBaby to true, if toy is triggering baby collider
        private void CheckIfTriggerBaby(Baby baby)
        {
            if (!baby.IsPlaying)
            {
                _triggerBaby = true;
                baby.OnStopPlayingWithToys += BabyFinishedPlaying;
            }
        }

        //Sets bool _triggerBaby to false, if toy is not triggering baby collider
        private void CheckIfExitBaby(Baby baby)
        {
            _triggerBaby = false;
        }

        //Makes this gameobject inactive during the baby playing with toy
        private void GiveBabyAToy()
        {
            if (_triggerBaby)
            {
                gameObject.SetActive(false);
                OnGiveBabyAToy?.Invoke();
            }
        }

        //Invoke in baby when it finished playing with toy
        public void BabyFinishedPlaying()
        {
            gameObject.SetActive(true);
        }
    }
}
