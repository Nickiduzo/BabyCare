using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : BaseHoleable
{
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] List<Sprite> _sprites;
    [SerializeField] int _splashCount;
    [SerializeField, Range(.1f, 1f)] float _burstDuration, _burstScale;
    [SerializeField, Range(-1f, 0f)] float _burstScaleMove;

    // monitor whether bomb is blewed ([Die] invoke when we tap on mole)
    private void Awake()
    {
        _renderer.sprite = _sprites[Random.Range(0, _sprites.Count)];

        transform.localScale = Vector3.zero;
        //MouseTrigger.OnDown += Burst;
    }

    //private void OnDestroy()
    //    => MouseTrigger.OnDown -= Burst;

    public override Tween Appear()
    {
        Collider.enabled = true;
        transform.localScale = scale;

        var pos = transform.localPosition.y - AppearOffsetMoving;
        var appearSequence = DOTween.Sequence();
        appearSequence.Append(transform.DOMoveY(pos + .5f, AppearDuration));
        appearSequence.Append(transform.DOMoveY(pos, AppearDuration));

        var appearTween = transform.DOScale(scale, AppearDuration);
        appearSequence.Join(appearTween);
        appearTween.OnComplete(() =>
        {
            var sequence = DOTween.Sequence();
            sequence.Append(DOVirtual.DelayedCall(UnityEngine.Random.Range(MinMaxHideDelay.x, MinMaxHideDelay.y), Hide));
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

        PlaySound("Put");
        PlaySound("FruitSquish");
        OnCatch?.Invoke();

        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() => ShowDieFx());
        sequence.Append(transform.DOScaleY(_burstScale, _burstDuration));
        sequence.Join(transform.DOBlendableMoveBy(new Vector3(0, _burstScaleMove, 0), _burstDuration));
        sequence.AppendCallback(() => { DisableObject(); isHitting = false; });
    }

    protected override void ShowDieFx(string name = null)
    {
        SplashSpawner.Instance.SpawnSplash(Random.Range(1, _splashCount));
    }
}
