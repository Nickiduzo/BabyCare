using Inputs;
using Sound;
using System;
using UnityEngine;


namespace Feeding
{
    public class Spoon : MonoBehaviour
    {
        public event Action Dragging;

        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] public DragAndDrop _dragAndDrop;

        [SerializeField] public InputSystem _inputSystem;
        [SerializeField] private SoundSystem _soundSystem;

        public bool drop; // flag what Drag End

        private void Awake()
        {
            Construct(_inputSystem, _soundSystem);
        }

        //when construct Spoon gameObject
        public void Construct(InputSystem inputSystem, SoundSystem soundSystem)
        {
            _inputSystem = inputSystem;
            _soundSystem = soundSystem;

            _dragAndDrop.Construct(inputSystem);
            _destinationOnDragEnd.Construct(transform.position);
            _dragAndDrop.OnDragStart += DragStart;
            _dragAndDrop.OnDragEnded += DragEnd;
            _destinationOnDragEnd.OnMoveComplete += DragStart;
            _dragAndDrop.OnDragStart += PlaySound;
        }

        private void PlaySound()
        {
            _soundSystem.PlaySound("spoon");
        }

        public void DragEnd()
        {
            drop = true;
            _soundSystem.StopSound("spoon");
        }

        private void DragStart()
        {
            drop = false;
        }
    }
}
