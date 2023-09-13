using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Baloon : BaseHoleable
{
    private const string SUCCESS = "Success";

    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] List<Sprite> _sprites;
    [SerializeField, Range(.1f, 1f)] float _popDuration, _popScale;
    [SerializeField, Range(-1f, 0f)] float _popScaleMove;

    // monitor whether bomb is blewed ([Die] invoke when we tap on mole)
    private void Awake()
    {
        _renderer.sprite = _sprites[Random.Range(0, _sprites.Count)];

        transform.localScale = Vector3.zero;
        //MouseTrigger.OnDown += Pop;
    }

    //private void OnDestroy()
    //    => MouseTrigger.OnDown -= Pop;

    public override Tween Appear()
    {
        Collider.enabled = true;
        transform.localScale = scale;

        PlaySound("BaloonAppeared");

        var pos = transform.localPosition.y - AppearOffsetMoving;
        var appearSequence = DOTween.Sequence();
        appearSequence.Append(transform.DOMoveY(pos + .5f, AppearDuration));
        appearSequence.Append(transform.DOMoveY(pos, AppearDuration));

        var appearTween = transform.DOScale(scale, AppearDuration);
        appearSequence.Join(appearTween);
        appearTween.OnComplete(() =>
        {
            var sequence = DOTween.Sequence();
            sequence.Append(DOVirtual.DelayedCall(Random.Range(MinMaxHideDelay.x, MinMaxHideDelay.y), Hide));
            sequence.Join(transform.DOLocalMoveY(transform.localPosition.y + 0.05f, 1f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo));

            hideTween = sequence;

        });
        return appearTween;
    }

    public override void Hide()
    {
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

    protected override void Hit()
    {
        if (hideTween != null && hideTween.IsActive())
        {
            hideTween.Kill();
        }

        //PlaySound(SUCCESS); //TODO: Burst sound
        PlaySound("Put");
        PlaySound("BaloonPop");
        OnCatch?.Invoke(); //TODO: Add plus time on 2nd baloon each time

        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() => ShowDieFx(SUCCESS));
        sequence.Append(transform.DOScaleY(_popScale, _popDuration));
        sequence.Join(transform.DOBlendableMoveBy(new Vector3(0, _popScaleMove, 0), _popDuration));
        sequence.AppendCallback(() => { DisableObject(); isHitting = false; });
    }
}
