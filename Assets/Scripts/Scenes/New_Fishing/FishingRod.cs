using DG.Tweening;
using Inputs;
using Sound;
using UnityEngine;
using UsefulComponents;

namespace NewFishing
{
    public class FishingRod : MonoBehaviour
    {
        [SerializeField] public DragAndDrop _dragAndDrop;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] public GameObject rod;
        [SerializeField] public Collider2D rodCollider;
        [SerializeField] public Hook _hook;
        [SerializeField] private MouseTrigger _mouseTrigger;
        private Tween _movingToDestination;
        private Vector3 _destination;
        private Vector2 _clampedYPosition = default(Vector2);
        private Vector3 oldPosition = default(Vector3);
        public bool fishingRodDragFlag = false;
        private bool fishingRodMoveToDestinationFlag = true;

        private void OnDestroy() => _dragAndDrop.OnDragEnded -= MoveToDestination;

        //when construct bottle gameObject
        public void Construct(InputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _dragAndDrop.Construct(inputSystem);
            _destination = transform.position;
            _dragAndDrop.OnDragEnded += MoveToDestination;
            _dragAndDrop.OnDrag += rodDrageble;
            _dragAndDrop.OnDragEnded += rodEndDrageble;
            _mouseTrigger.OnUp += rodEndDrageble;
        }

        // on slow DragAndDrop
        public void StartSlowDragMove()
        {
            _dragAndDrop.IsDraggable = false;
            _mouseTrigger.OnUp += MoveToDestination;
            _mouseTrigger.OnUp += StopSlowDragMove;      
            _mouseTrigger.OnDrag += SlowDragMove;
            oldPosition = default(Vector3);
        }
        // off slow DragAndDrop
        public void StopSlowDragMove()
        {
            _mouseTrigger.OnDrag -= SlowDragMove;
            _mouseTrigger.OnUp -= StopSlowDragMove;
            _mouseTrigger.OnUp -= MoveToDestination;        
            oldPosition = default(Vector3);
            _dragAndDrop.IsDraggable = true;
        }

        //DragAndDrop with slow motion
        public void SlowDragMove()
        {
            Vector3 newPosition = _inputSystem.CalculateTouchPosition();
            
            if ((rod.transform.position.x - newPosition.x + rod.transform.position.y - newPosition.y) <= 0.1f && (rod.transform.position.x - newPosition.x + rod.transform.position.y - newPosition.y)> 0)
            {
                StopSlowDragMove();
                return;
            }
            if (_clampedYPosition != default(Vector2))
            {
                newPosition.y = Mathf.Clamp(newPosition.y, _clampedYPosition.x, _clampedYPosition.y);
            }

            if (oldPosition != default(Vector3))
            {
                rod.transform.position = new Vector3(rod.transform.position.x - (rod.transform.position.x - newPosition.x) / 96 - (oldPosition.x - newPosition.x), rod.transform.position.y - (rod.transform.position.y - newPosition.y) / 96 - (oldPosition.y - newPosition.y), newPosition.z);
            }
            else
            {
                rod.transform.position = new Vector3(rod.transform.position.x - (rod.transform.position.x - newPosition.x) / 96, rod.transform.position.y - (rod.transform.position.y - newPosition.y) / 96, newPosition.z);
            }

            oldPosition = newPosition;
        }
        // off Move To Destination On Drag End 
        public void StopMoveToDestination()
        {
            fishingRodMoveToDestinationFlag = false;
            _mouseTrigger.OnUp -= MoveToDestination;
            _dragAndDrop.OnDragEnded -= MoveToDestination;
        }
        // on Move To Destination On Drag End 
        public void StartMoveToDestination()
        {
            fishingRodMoveToDestinationFlag = true;
            _mouseTrigger.OnUp += MoveToDestination;
            _dragAndDrop.OnDragEnded += MoveToDestination;
        }

        //Move To Destination On Drag End 
        public void MoveToDestination()
        {
            if (fishingRodMoveToDestinationFlag)
            {
                _movingToDestination = rod.transform.DOMove(_destination, 0.5f)
                .OnComplete(() => { });
            }
        }

        //disables all interactions with the fishing rod
        public void StopAllActions()
        {
            MoveToDestination();
            _dragAndDrop.IsDraggable = false;
            rodCollider.enabled = false;
            _dragAndDrop.OnDragEnded -= MoveToDestination;
            _mouseTrigger.OnDrag -= SlowDragMove;
            _mouseTrigger.OnUp -= StopSlowDragMove;
            _mouseTrigger.OnUp -= MoveToDestination;
        }

        //on flag when fishing rod Drag
        private void rodDrageble()
        {
            fishingRodDragFlag = true;
        }

        //off flag when fishing rod Drag
        private void rodEndDrageble()
        {
            fishingRodDragFlag = false;
        }
    }
}
