using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class AppearWithScale : AppearWithDuration
    {
        protected Vector3 _appearScale;

        private void Awake()
        {
            _appearScale = transform.localScale;
        }
        public override Tween Appear()
            => transform.DOScale(_appearScale, _appearDuration);
    }
}