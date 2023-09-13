using DG.Tweening;
using UnityEngine;

namespace Fx
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private float _appearDuration;
        [SerializeField] private float _selfDestroyInterval;

        private bool isPlaying = false;

        // Effect lifetime sequence;
        public void Appear()
        {
            AppearWithScale();
            AppearWithSprite();
        }

        public void Play()
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            isPlaying = true;
            var sequence = DOTween.Sequence();
            sequence.Append(_sprite.DOFade(1, _appearDuration));
            sequence.AppendInterval(_selfDestroyInterval);
            sequence.Append(_sprite.DOFade(0, _appearDuration));
            sequence.AppendCallback(Reset);
        }

        private void Reset() => isPlaying = false;

        private void AppearWithSprite()
        {
            gameObject.SetActive(true);
            var sequence = DOTween.Sequence();
            sequence.Append(_sprite.DOFade(1, _appearDuration));
            sequence.AppendInterval(_selfDestroyInterval);
            sequence.Append(_sprite.DOFade(0, _appearDuration));
            sequence.AppendCallback(Disable);
        }

        private void AppearWithScale()
        {
            Vector3 cachedScale = transform.localScale;
            transform.localScale = Vector3.zero;

            float random = GetRandomScaleMultiplier();

            transform.DOScale(cachedScale * random, _appearDuration);
        }

        private float GetRandomScaleMultiplier()
            => Random.Range(0.3f, 1f);

        private void Disable()
            => gameObject.SetActive(false);
    }
}