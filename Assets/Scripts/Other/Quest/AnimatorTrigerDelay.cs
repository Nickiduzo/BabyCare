using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorTrigerDelay : MonoBehaviour
{
    [SerializeField] private string _triger;
    [SerializeField] private float _delay;
    private Animator _animator;

    // enable animation so that the farmer waves to shopper when shopper leaves 
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        InvokeRepeating(nameof(ActiveTrigger), _delay, _delay);
    }
    
    private void ActiveTrigger()
    { 
        _animator.SetTrigger(_triger);
    }
}
