using Inputs;
using UnityEngine;

namespace PlayScene
{
    public class Child : MonoBehaviour
    {
        [SerializeField] public ChildView _ChildView;
        [SerializeField] public Transform _ToyparentPart;
        [SerializeField] public Transform _ToyparentPartHand;
        
        public void Construct()
        {

        }
    }
}
