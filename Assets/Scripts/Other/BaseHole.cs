using DG.Tweening;
using UnityEngine;
using UsefulComponents;

//Uses to fill containers by editor extensions
public class BaseHole : MonoBehaviour
{
    [SerializeField] protected Collider2D _selfCollider;

    protected IProgressCalculator _progressCalculator;

    // starts smooth disappearance of hole
    public Tween Disappear()
        => transform.DOScale(Vector3.zero, 0.5f);

    // disable collider, delete all Tweens
    protected void MakeNonInteractable()
    {
        DOTween.Kill(gameObject);
        _selfCollider.enabled = false;
    }

    // enable collider
    public void MakeInteractable()
        => _selfCollider.enabled = true;

    // invoke [addProgress] in IProgressCalculator pass [deltaTime] parameter
    public virtual void AddProgress(float deltaTime)
    {
        _progressCalculator.AddProgress(deltaTime);
    }

    public virtual void PlayGroundFX()
    {
        FxSystem.Instance.PlayEffect("Ground", transform.position);
    }

    public virtual void PlaySuccessFX()
    {
        FxSystem.Instance.PlayEffect("Success", transform.position);
    }
}