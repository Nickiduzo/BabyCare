using System;
using UnityEngine;

namespace NewFishing
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hook : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _fishMass;

        public bool WithFish => _fishMass > 0;

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody2D>();

        public void HookUp(float fishMass)
        {
            if (fishMass <= 0)
                throw new ArgumentOutOfRangeException(nameof(fishMass));
            if (WithFish)
                throw new InvalidOperationException(nameof(HookUp));

            _fishMass = fishMass;
            _rigidbody.mass += _fishMass;
        }

        public void Unhook()
        {
            if (!WithFish)
                throw new InvalidOperationException(nameof(Unhook));

            _rigidbody.mass -= _fishMass;
            _fishMass = 0;
        }
    }
}