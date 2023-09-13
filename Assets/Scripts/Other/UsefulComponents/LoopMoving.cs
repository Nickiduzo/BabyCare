using DG.Tweening;
using UnityEngine;

namespace UsefulComponents
{
    public class LoopMoving : MonoBehaviour
    {
        [SerializeField] private float _movingDuration;
        [SerializeField] private Ease _movingType;

        private Tween _movingTween;
        private Vector3 _destination;
        private Vector3 _startPosition;


        public void Construct(Vector3 destination)
        {
            _startPosition = transform.position;
            _destination = destination;
        }


        public void StartMoving()
        {
            _movingTween = MoveTo(_destination)
                .SetLoops(-1, LoopType.Yoyo)
                .OnStepComplete(() =>
                {
                    if (transform.eulerAngles.y == 0f)
                    {
                        transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                    }
                });
        }

        public void Kill()
        {
            _movingTween.Kill();
            _movingDuration = 4f;
            _movingTween = MoveTo(_startPosition)
                .OnComplete(() => gameObject.SetActive(false));
        }

        private Tween MoveTo(Vector3 point)
        {
            Vector3 direction = (point - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (direction.x * transform.localScale.x < 0)
            {
                transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }

            return transform.DOMove(point, _movingDuration).SetEase(_movingType);
        }
    }
}