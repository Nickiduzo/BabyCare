using DG.Tweening;
using Sound;
using UnityEngine;

namespace UI
{
    public abstract class HudElement : MonoBehaviour
    {
        [SerializeField] private float _appearDuration;
        [SerializeField] private RectTransform _appearPosition;
        [SerializeField] protected SoundSystem _soundSystem;
        private RectTransform _rectTransform;

        protected Vector3 _cachedPosition;

        protected void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _cachedPosition = _rectTransform.anchoredPosition;
        }
        public void Appear()
        {
            _rectTransform.DOAnchorPos(_appearPosition.anchoredPosition, _appearDuration).SetEase(Ease.OutBack);
        }

        public void Disappear()
        {
            _rectTransform.DOAnchorPos(_cachedPosition, _appearDuration).SetEase(Ease.InBack);
        }

        public void Click()
        {
            _soundSystem.PlaySound("buttonUIClick");
            Vector3 cachedScale = _rectTransform.localScale;
            _rectTransform.DOScale(_rectTransform.localScale * 0.8f, 0.1f)
                .OnComplete(() => {
                    _rectTransform.DOScale(cachedScale, 0.1f);
                });

        }
    }
}