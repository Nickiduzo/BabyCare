﻿using UnityEngine;

namespace Mole
{
    public class WaterPumpStream : MonoBehaviour
    {
        [SerializeField] private BaseHoleTriggerObserver _observer;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _sprite;
        
        // initializes water stream in [Sprinkler]
        private void Start()
            => Sprinkler.Instance.InitCarrotStream(_observer, _collider, _sprite);
    }
}
