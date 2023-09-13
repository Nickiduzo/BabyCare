using System.Collections;
using UnityEngine;

public class PuzzleModel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private string _touchTriggerName = "Touch";
    private string _appearTriggerName = "Appear";

    private bool _firstTimeAppear = true;
    private void OnEnable()
    {
        if (_firstTimeAppear)
        {
            _firstTimeAppear = false;
            _animator.SetTrigger(_appearTriggerName);
        }
        else
        {
            _animator.SetTrigger(_touchTriggerName);
        }
    }
}
