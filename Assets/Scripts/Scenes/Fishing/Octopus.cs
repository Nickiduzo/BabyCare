using UnityEngine;

namespace Fishing
{
    public class Octopus : MonoBehaviour, IUncaughtable
    {
        private const string CaughtKey = "Caught";
        [SerializeField] private Animator _anim;
        [SerializeField] private FxSystem _fxSystem;

        public void Caught()
        {
            _anim.SetTrigger(CaughtKey);
            InitParticles();
        }

        private void InitParticles()
        {
            _fxSystem.PlayEffect("Octopus", transform.position);
        }
        
    }
}