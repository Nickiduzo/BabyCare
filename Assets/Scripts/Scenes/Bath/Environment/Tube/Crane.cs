using DG.Tweening;
using Sound;
using System;
using UnityEngine;

namespace Bath
{
    public class Crane : MonoBehaviour
    {
        public event Action OnTurnOnTube;
        public event Action OnTurnOffTube;

        [SerializeField] private MouseTrigger _mouseTrigger;
        [SerializeField] private Collider2D _collider;

        private SoundSystem _sound;

        public void Construct(SoundSystem sound)
        {
            _sound = sound;
            _mouseTrigger.OnUp += TurnOnCrane;
        }

        //Turning crane on 90 degrees and invokes tube method to start water particle
        private void TurnOnCrane()
        {
            _sound.PlaySound("CraneOn");
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(TogleCollider);
            sequence.Append(transform.DORotate(new Vector3(0, 0, 90), 1));
            sequence.AppendCallback(TogleCollider);
            
            OnTurnOnTube?.Invoke();
            _mouseTrigger.OnUp -= TurnOnCrane;
            _mouseTrigger.OnUp += TurnOffCrane;
        }

        //Turning crane on -90 degrees and invokes tube method to stop water particle
        private void TurnOffCrane()
        {
            _sound.PlaySound("CraneOff");
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(TogleCollider);
            sequence.Append(transform.DORotate(new Vector3(0, 0, 0), 1));
            sequence.AppendCallback(TogleCollider);
            
            OnTurnOffTube?.Invoke();
            _mouseTrigger.OnUp -= TurnOffCrane;
            _mouseTrigger.OnUp += TurnOnCrane;
        }

        //Making crane non interactable
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
