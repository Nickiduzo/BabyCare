using UnityEngine;

public class ChildAnimationController : MonoBehaviour
{
    [SerializeField] ChildView _child;
    private IAttachable obj;

    private void Start()
    {
        _child.OnTriggerEnter += AttachedAnimOnDragEnd;
        _child.OnTriggerExit += AttachedAnimOnExit;
        IndependentToyView.OnToyAnim += IndependentAnim;
        HintController.Instance.OnHint += HintAnim;
    }

    private void OnDestroy() => IndependentToyView.OnToyAnim -= IndependentAnim;

    private async void AttachedAnimOnDragEnd(IAttachable _obj)
    {
        obj = _obj;
        obj.dragAndDrop.OnDragEnded += AttachedAnim;
    }
    private async void AttachedAnimOnExit(IAttachable _obj)
    {
        //  obj = null;
        obj.dragAndDrop.OnDragEnded -= AttachedAnim;
    }

    /// <summary>
    /// Child animation for objects attached to Child
    /// </summary>
    /// <param name="obj">Attached object</param>
    private async void AttachedAnim()
    {
        if (ToyService.IsPlaying || !obj.IsAttachable) return;
        ToyService.IsPlaying = true;
        _child.OnTriggerEnter -= AttachedAnimOnDragEnd;
        // _child.OnTriggerStay -= AttachedAnim;

        _child.Animator.StopPlayback();

        await obj.OnAnimStart();
        _child.Animator.SetTrigger(obj.KindOfObject);

        WaitForAnimationState waitForAnimationState = _child.Animator.GetBehaviour<WaitForAnimationState>();

        await waitForAnimationState.WaitUntilAnimationFinished();

        _child.OnTriggerEnter += AttachedAnimOnDragEnd;
        obj.OnAnimEnd();
    }

    public void OnAnimEnd()
    {
        ToyService.IsPlaying = false;
    }

    /// <summary>
    /// Child animation for child-independent objects
    /// </summary>
    private void IndependentAnim()
    {
        _child.Animator.StopPlayback();
        _child.Animator.SetTrigger("Joyful1");
    }

    /// <summary>
    /// Child animation when Hint appears
    /// </summary>
    private void HintAnim()
    {
        _child.Animator.SetTrigger("Hint");
    }
}
