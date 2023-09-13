using System;
using Inputs;
using Sound;
using DG.Tweening;
using UnityEngine;

namespace Bath
{
    public class BubbleStick : MonoBehaviour
    {
        private InputSystem _inputSystem;
        private SoundSystem _soundSystem;

        [SerializeField] private DragAndDrop _dragAndDrop;
        [SerializeField] private MoveToDestinationOnDragEnd _moveToDestinationOnDragEnd;
        [SerializeField] private ParticleSystem _bubble;
        [SerializeField, Range(0, 1)] private float _activateTime;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField, Range(5, 40)] private float _spawnBubbleSpeed;
        private Vector3 _oldPosition;

        static public bool isStick = false;
        //Main construct for pool
        public void Construct(InputSystem inputSystem, SoundSystem soundSystem)
        {
            _inputSystem = inputSystem;
            _soundSystem = soundSystem;

            _dragAndDrop.Construct(_inputSystem);
            _dragAndDrop.OnDragStart += BubbleSpawn;
            _dragAndDrop.OnDragStart += Show;
            _dragAndDrop.OnDragEnded += StopBubble;
            _dragAndDrop.OnDragEnded += Hide;

            _moveToDestinationOnDragEnd.Construct(transform.position);

            _oldPosition = transform.position;

            StopBubble();
        }

        //CanSpawnBubble if Draggable and traveled distance per frame >= _spawnBubbleSpeed
        private bool CanSpawnBubble =>
            _dragAndDrop.IsDraggable &&
            Vector3.Distance(_oldPosition, transform.position) / Time.deltaTime >= _spawnBubbleSpeed;

        private void BubbleSpawn()
        {
            _bubble.Play();
        }

        private void StopBubble()
        {
            isStick = false;
            _bubble.Stop();
        }
        //Work on start and each fixed step, spawn bubble when use stick
        private void FixedUpdate()
        {
            if (CanSpawnBubble)
            {
                isStick = true;
                BubbleSpawn();
            }

            _oldPosition = transform.position;
        }

        //Show stick
        private void Show() =>
            _sprite.DOFade(1, _activateTime);

        //Hide stick
        private void Hide() =>
            _sprite.DOFade(0, _activateTime);
    }
}