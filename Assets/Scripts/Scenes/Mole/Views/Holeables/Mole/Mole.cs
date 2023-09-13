using DG.Tweening;
using Sound;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Mole
{
    public class Mole : BaseHoleable, ICollectable
    {
        private const string SUCCESS = "Success";
        private System.Random rand = new System.Random();
        private int hitType;

        [SerializeField] int _scorePoints;
        [SerializeField] GameObject _deadFX;

        public int ScorePoints => _scorePoints;

        // monitor whether mole is dead ([Die] invoke when we tap on mole)
        private void Awake()
        {
            transform.localScale = Vector3.zero;
            //MouseTrigger.OnDown += Die;
        }

        //private void OnDestroy()
        //    => MouseTrigger.OnDown -= Die;

        // appear mole, set mole sprite and scale, play eyes anim, set delay before hiding
        public override Tween Appear()
        {
            OffDeadFx();
            Animator.SetTrigger("Off");
            Collider.enabled = true;
            transform.DOMoveY(transform.localPosition.y - AppearOffsetMoving, AppearDuration);
            var appearTween = transform.DOScale(scale, AppearDuration);
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

        // smooth downscaling to zero for body and hands
        protected override Tween Disappear()
        {
            var sequence = DOTween.Sequence();
            
            sequence.Append(transform.DOScale(Vector3.zero, AppearDuration));

            return sequence;
        }


        // disable dead eye sprites
        private void OffDeadFx()
            => _deadFX.SetActive(false);

        // set dead mole sprite, turn on stars FX above mole, play "Success" and "Put" sounds, enable "deadEyesAnim", 
        // after all these actions, mole move to under hole and disappear/disable
        protected override void Hit()
        {
            if (hideTween != null && hideTween.IsActive())
            {
                hideTween.Kill();
            }

            HideRenderer();
            OnCatch?.Invoke();
            _deadFX.SetActive(true);
            //PlaySound(SUCCESS);

            PlaySound("Put");

            hitType = rand.Next(2);
            if (hitType == 0)
                PlaySound("MoleHit");
            else if (hitType == 1)
                PlaySound("MoleHit1");

            Animator.SetTrigger("Hit");

            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => ShowDieFx(SUCCESS));
            sequence.Join(Stretch());
            sequence.AppendCallback(OffDeadFx);
            sequence.Append(MoveOff());
            sequence.Join(Disappear());
            sequence.AppendCallback(() => { DisableObject(); isHitting = false; });
        }
        
        // a bit shake mole from side to side
        private Tween Stretch()
        {
            var sequence = DOTween.Sequence();
            sequence.Join(transform.DOLocalMoveX(transform.localPosition.x - 0.08f, 0.3f)
                .SetEase(Ease.InOutSine)
                .SetLoops(4, LoopType.Yoyo));

            return sequence;
        }

        // start hiding under hole
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
    }
}