using DG.Tweening;
using Sound;
using UnityEngine;

namespace Bath
{
    public class Duck : MonoBehaviour
    {
        [SerializeField] private MouseTrigger _mouseTrigger;
        [SerializeField] private Collider2D _collider;

        private SoundSystem _soundSystem;
        //Main construct for pool
        public void Construct(SoundSystem soundSystem)
        {
            _soundSystem = soundSystem;
            _mouseTrigger.OnUp += PlayingDuck;
        }
        //Works of first frame of start game
        private void Start()
        {
            IdleAnim();
        }

        //Starts the loop idle animation
        private void IdleAnim()
        {
            transform.DOMoveY(-0.95f, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveX(1.9f, 6).SetEase(Ease.Linear));
            sequence.AppendCallback(ChangeScale);
            sequence.Append(transform.DOMoveX(-2.8f, 6).SetEase(Ease.Linear));
            sequence.AppendCallback(ChangeScale);
            sequence.Play().SetLoops(-1);
        }

        //Turing duck in other direction, so it go towards looking
        private void ChangeScale()
        { 
            transform.localScale = new Vector3(transform.localScale.x * -1, 0.7f, 1);
        }

        //Interact with duck
        private void PlayingDuck()
        {
            _soundSystem.PlaySound("Duck");
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(TogleCollider);
            sequence.Append(transform.DOMoveY(-1.11f, 0.4f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo));
            sequence.AppendCallback(TogleCollider);
            sequence.Play();
        }

        //Make duck non interactable
        private void TogleCollider()
        {
            if (_collider.enabled)
            {
                _collider.enabled = false;
            }
            else
            {
                _collider.enabled = true;
            }
        }
    }
}
