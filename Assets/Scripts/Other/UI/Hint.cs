using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private float _appearDuration;

        private Vector3 _cachedScale;

        private void Awake()
            => Init();

        private void Init()
        {
            _cachedScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        public void Move(Vector3 start, Vector3 end)
        {
            // while()
        }

        public void Appear()
            => transform.DOScale(_cachedScale, _appearDuration);

        public void Disappear()
            => transform.DOScale(Vector3.zero, _appearDuration);
    }
}