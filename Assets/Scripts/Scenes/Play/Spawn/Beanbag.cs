using Inputs;
using UnityEngine;
using UsefulComponents;
namespace PlayScene
{
    public class Beanbag : MonoBehaviour
    {
        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] public DragAndDrop _dragAndDrop;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] public BaseToyView _BaseToyView;
        public Transform _ParentPart { get; set; }
        public ChildView _Child { get; set; }

        // Start is called before the first frame update
        public void Awake()
        {
           // Construct(_inputSystem);
        }

        //when construct gameObject
        public void Construct(InputSystem inputSystem)
        {
           // gameObject.transform.SetParent(null);
            _inputSystem = inputSystem;
            _BaseToyView.InputSystem = inputSystem;
            _BaseToyView.DragAndDrop = _dragAndDrop;

        }
        public void SetValue()
        {
            _BaseToyView.Destination = transform.position;
            _BaseToyView.ParentPart = _ParentPart;
            _BaseToyView.Child = _Child;
        }
    }
}
