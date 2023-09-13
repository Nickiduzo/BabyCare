using Inputs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketView : CollisionObserverWithPayload<ObstacleView>
{
    [SerializeField] Animator _animator;
    [SerializeField] InputSystem _inputSystem;
    [SerializeField, Range(0f, 100f)] float _speedForward;
    [SerializeField, Range(5f, 20f)] float _speedForwardLimit;
    [SerializeField, Range(0f, 100f)] float _speedUpDown;
    [SerializeField, Range(0.001f, 1f)] float _timeToIncreaseSpeed;
    [SerializeField, Range(0.001f, 1f)] float _valueToDecreaseSpeed;
    [SerializeField, Range(0.001f, 1f)] float _valueToIncreaseSpeed;

    public float SpeedForward { get { return _speedForward; } set { _speedForward = value; } }
    public float InitSpeedForward { get; private set; }
    public float ValueToDecreaseSpeed => _valueToDecreaseSpeed;
    public float SpeedUpDown => _speedUpDown;
    public Animator Animator => _animator;
    public InputSystem InputSystem => _inputSystem;

    private void Start()
    {
        InitSpeedForward = SpeedForward;

        StartCoroutine(IncreaseSpeed());
    }

    IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToIncreaseSpeed);
            _speedForward += _valueToIncreaseSpeed;

            //Break if reached the limit
            if(_speedForward >= _speedForwardLimit)
                yield break;
        }
    }
}
