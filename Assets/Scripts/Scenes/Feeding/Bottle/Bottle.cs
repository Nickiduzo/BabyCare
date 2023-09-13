using Inputs;
using UnityEngine;
using UsefulComponents;

namespace Feeding
{
    public class Bottle : MonoBehaviour
    {
        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] public DragAndDrop _dragAndDrop;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] public GameObject shadow;

        public bool drop; // flag what Drag End

        public void Awake()
        {
            Construct(_inputSystem);

        }

        //when construct bottle gameObject
        public void Construct(InputSystem inputSystem)
        {
            _dragAndDrop.Construct(inputSystem);
            _destinationOnDragEnd.Construct(transform.position);
            _dragAndDrop.OnDragStart += DragStart;
            _dragAndDrop.OnDragEnded += DragEnd;
            _destinationOnDragEnd.OnMoveComplete += OnShadow;
        }

        public void DragEnd()
        {
            drop = true;
        }
        private void OnShadow()
        {
            shadow.SetActive(true);
            drop = false;
        }

        private void DragStart()
        {
            shadow.SetActive(false); // off shadow
            drop = false;
        }


        //disable hint when player tap bottle
        private void OnMouseDown()
        {
            HintSystem.Instance.HidePointerHint();
        }

    }
}
