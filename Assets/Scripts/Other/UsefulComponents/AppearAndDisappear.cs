using DG.Tweening;
using UnityEngine;

namespace UsefulComponents
{
    public class AppearAndDisappear : MonoBehaviour
    {

        [SerializeField] private float _appearDuration;
        private Vector3 _cachedScale;

        private void Awake()
            => _cachedScale = transform.localScale;

        public Tween Appear()
            => transform.DOScale(_cachedScale, _appearDuration);

        public Tween Disappear()
            => transform.DOScale(Vector3.zero, _appearDuration);
    }
}