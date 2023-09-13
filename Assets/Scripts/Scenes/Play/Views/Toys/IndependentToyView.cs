using System;
using System.Threading.Tasks;
using UnityEngine;

public class IndependentToyView : BaseToyView
{
    public static event Action OnToyAnim;

    [SerializeField] private Animator _animator;
    private bool _active = true;

    private new void Start()
    {
        InputSystem.OnTapped += TapOnToy;
    }

    private void OnDestroy()
    {
        InputSystem.OnTapped -= TapOnToy;
    }

    public override async Task OnAnimStart()
    {
        _animator.SetTrigger("Action");
        OnToyAnim?.Invoke();
        _active = false;
        WaitForAnimationState waitForAnimationState = Child.Animator.GetBehaviour<WaitForAnimationState>();

        await waitForAnimationState.WaitUntilAnimationFinished();
    }

    private async void TapOnToy(Vector3 tapPos)
    {
        if (ToyService.IsPlaying || !_active)
            return;

        DeactivateHint();

        var hit = Physics2D.Raycast(tapPos, Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent(out IndependentToyView toy))
        {
            if (toy.KindOfToy == KindOfToy)
            {
                ToyService.IsPlaying = true;
                MakeAllNonInteractable();
                await OnAnimStart();
                _active = true;
            }
        }
    }

    public override void OnAnimEnd()
    {
        ToyService.IsPlaying = false;
        MakeAllInteractable();
    }
}