using Inputs;
using UnityEngine;


namespace Feeding
{
    public class Cookies : MonoBehaviour
    {
        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] public DragAndDrop _dragAndDrop;
        [SerializeField] private InputSystem _inputSystem;

        public bool drop; // flag what Drag End
        public void Awake()
        {
            Construct(_inputSystem);
        }

        //when construct Cookies gameObject
        public void Construct(InputSystem inputSystem)
        {
            _dragAndDrop.Construct(inputSystem);
            _destinationOnDragEnd.Construct(transform.position);
            _dragAndDrop.OnDragStart += DragStart;
            _dragAndDrop.OnDragEnded += DragEnd;
            _destinationOnDragEnd.OnMoveComplete += DragStart;

        }

        public void DragEnd()
        {
            drop = true;
        }

        private void DragStart()
        {
            drop = false;
        }
    }
}
