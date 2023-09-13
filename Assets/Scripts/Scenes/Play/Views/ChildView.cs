using UnityEngine;

public class ChildView : TriggerObserverWithPayload<IAttachable>
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [SerializeField] private string _strokeTrigger;
    [SerializeField] private RandomSound _strokeSound;

    private void OnMouseDown()
    {
        if (ToyService.IsPlaying)
            return;
        
        Animator.SetTrigger(_strokeTrigger);
        _strokeSound.Play();
    }
}