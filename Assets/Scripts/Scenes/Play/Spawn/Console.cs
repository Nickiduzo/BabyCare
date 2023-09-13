using Inputs;
using Scene;
using UnityEngine;
using UsefulComponents;

namespace PlayScene
{
    public class Console : MonoBehaviour
    {
        [SerializeField] private MoveToDestinationOnDragEnd _destinationOnDragEnd;
        [SerializeField] public DragAndDrop _dragAndDrop;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] public BaseToyView _BaseToyView;
        [SerializeField] public ConsoleView _ConsoleView;
        public SceneLoader _sceneLoader { get; set; }
        public Transform _ParentPart { get; set; }
        public ChildView _Child { get; set; }
        // Start is called before the first frame update
        public void Awake()
        {
          //  Construct(_inputSystem);
        }

        //when construct gameObject
        public void Construct(InputSystem inputSystem)
        {
          //  gameObject.transform.SetParent(null);
            _inputSystem = inputSystem;
            _BaseToyView.InputSystem = inputSystem;
            _BaseToyView.DragAndDrop = _dragAndDrop;
        }
        public void SetValue()
        {
            _ConsoleView._sceneLoader = _sceneLoader;
            _BaseToyView.Destination = transform.position;
            _BaseToyView.ParentPart = _ParentPart;
            _BaseToyView.Child = _Child;
        }
    }
}
