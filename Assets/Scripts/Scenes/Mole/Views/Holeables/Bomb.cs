using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class Bomb : BaseHoleable
{
    [SerializeField] SelectRandom _spriteSelector;
    [SerializeField] SpriteRenderer _fireRenderer;
    [SerializeField] GameObject _explosion;
    [SerializeField] float _explosionDuration;

    private float hideDelay;

    // monitor whether bomb is blewed ([Explode] invoke when we tap on mole)
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        //MouseTrigger.OnDown += Explode;
    }

    //private void OnDestroy()
    //    => MouseTrigger.OnDown -= Explode;

    public override Tween Appear()
    {
        Collider.enabled = true;
        transform.localScale = scale;

        hideDelay = Random.Range(MinMaxHideDelay.x, MinMaxHideDelay.y);

        //PlaySound("BombWick");

        var pos = transform.localPosition.y - AppearOffsetMoving;
        var appearSequence = DOTween.Sequence();
        appearSequence.Append(transform.DOMoveY(pos + .5f, AppearDuration));
        appearSequence.Append(transform.DOMoveY(pos, AppearDuration));

        var appearTween = transform.DOScale(scale, AppearDuration);
        appearSequence.Join(appearTween);
        appearTween.OnComplete(() =>
        {
            var sequence = DOTween.Sequence();
            sequence.Append(DOVirtual.DelayedCall(hideDelay, Hide));
            sequence.Join(transform.DOLocalMoveY(transform.localPosition.y + 0.05f, 1f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo));

            hideTween = sequence;

        });
        return appearTween;
    }

    public override void Hide()
    {
        //StopSound("BombWick");
        Debug.Log("Hiding object");
        if (hideTween != null && hideTween.IsActive())
        {
            hideTween.Kill();
        }
        var sequence = DOTween.Sequence();
        sequence.Append(MoveOff());
        sequence.Join(Disappear());
        sequence.AppendCallback(HideObject);
    }

    protected async override void Hit()
    {
        if (hideTween != null && hideTween.IsActive())
        {
            hideTween.Kill();
        }

        //StopSound("BombWick");
        PlaySound("Put");
        PlaySound("BombExplosion");
        OnCatch?.Invoke();

        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            ShowDieFx();
            CameraEffects.Instance.ShakeOnce(_explosionDuration);
            _fireRenderer.enabled = false;
            _spriteSelector.gameObject.SetActive(false);
        });
        sequence.Join(_explosion.transform.DOScale(scale, _explosionDuration));
        await sequence.AppendCallback(() =>
        {
            HideDieFx();
            _fireRenderer.enabled = true;
            _spriteSelector.gameObject.SetActive(true);
            DisableObject();
            isHitting = false;
        }).AsyncWaitForCompletion();
    }

    protected override void ShowDieFx(string name = null)
    {
        _explosion.SetActive(true);
        _explosion.transform.parent = null;
    }
    protected void HideDieFx()
    {
        _explosion.SetActive(false);
        _explosion.transform.parent = transform;
    }
}
