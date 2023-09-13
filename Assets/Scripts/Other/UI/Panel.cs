using DG.Tweening;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Panel : MonoBehaviour
    {
        [SerializeField] private float _appearDuration;
        [SerializeField] private CanvasGroup _canvasGroup;

        protected Tween _appearPanelTween;

        public virtual void Appear()
        {
            _canvasGroup.alpha = 0;
            _appearPanelTween = _canvasGroup.DOFade(1, _appearDuration);
            _canvasGroup.interactable = true;
        }

        public virtual void Disappear()
        {
            _canvasGroup.DOFade(0, _appearDuration);
            _canvasGroup.interactable = false;
        }

    }
}