using Inputs;
using UnityEngine;

namespace PlayScene
{
    public class Dino : MonoBehaviour
    {
        [SerializeField] public BaseToyView _BaseToyView;
        public InputSystem _inputSystem { get; set; }
        public ChildView _Child { get; set; }

        //when сonstruct gameObject
        public void Construct()
        {

        }
        public void SetValue()
        {
            _BaseToyView.InputSystem = _inputSystem;
            _BaseToyView.Child = _Child;
        }
    }
}
