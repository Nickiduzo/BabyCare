using UnityEngine;
using UnityEngine.Animations;
using System;
using System.Threading.Tasks;
using UnityEngine.Events;

public class WaitForAnimationState : StateMachineBehaviour
{
    private static TaskCompletionSource<bool> _taskCompletionSource;
    private static bool _isCompleted;

    private void Awake()
    {
        _taskCompletionSource = new TaskCompletionSource<bool>();
    }

    /// <summary>
    /// Waits for the animation to finish
    /// </summary>
    /// <returns></returns>
    public async Task WaitUntilAnimationFinished()
    {
        _taskCompletionSource = new TaskCompletionSource<bool>();
        _isCompleted = false;

        await _taskCompletionSource.Task;

        _isCompleted = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!_isCompleted)
        {
            _taskCompletionSource.SetResult(_isCompleted);
        }
    }
}
