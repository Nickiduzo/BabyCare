using DG.Tweening;
using Inputs;
using System;
using Sound;
using UnityEngine;
using UsefulComponents;


namespace Thimble
{
    public class ThimbleCup : MonoBehaviour
    {
        public event Action OnStartShow;
        public event Action OnEndShow;
        public event Action OnEndMove;
        public event Action OnFindGoldenBall;
        public event Action OnDontFindGoldenBall;

        [SerializeField] private Collider2D _collider;
        [SerializeField] private GameObject GoldenBall;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private string _showSound;
        public bool withGoldenBall;

        private float _animationSpeed = 1;
        private float _horizontalTossSpeed;
        private float _verticalTossSpeed;

        //when construct cup gameObject
        public void Construct(bool _withGoldenBall, float horizontalTossSpeed, float verticalTossSpeed)
        {
            if (horizontalTossSpeed < 0)
                throw new ArgumentOutOfRangeException(nameof(horizontalTossSpeed));
            if (verticalTossSpeed < 0)
                throw new ArgumentOutOfRangeException(nameof(verticalTossSpeed));

            _horizontalTossSpeed = horizontalTossSpeed;
            _verticalTossSpeed = verticalTossSpeed;
            GoldenBall.SetActive(_withGoldenBall);
            withGoldenBall = _withGoldenBall;
            _soundSystem.StopSound(_showSound);
        }

        //stops the ability to interact
        public void StopInteractivity()
        {
            _collider.enabled = false;
        }

        //start the ability to interact
        public void StartInteractivity()
        {
            _collider.enabled = true;
        }

        //when end showing animation
        private void EndShowAnimation()
        {
            OnEndShow?.Invoke();
        }

        //in the middle of an animation
        private void MiddleShowAnimation()
        {
            if (withGoldenBall)
            {
                OnFindGoldenBall?.Invoke();
            }
            else
            {
                OnDontFindGoldenBall?.Invoke();
            }
        }

        //when start showing animation
        private void StartShowAnimation()
        {
            _soundSystem.PlaySound(_showSound);
            OnStartShow?.Invoke();
            StopInteractivity();
        }


        //ball showing animation
        public void MoveShowGoldenBall()
        {
            StopInteractivity();
            Sequence show = DOTween.Sequence();
            // show.AppendCallback(startShowAnimation);
            show.Append(transform.DOMoveY(transform.position.y + 1, _animationSpeed ));
            show.Join(GoldenBall.transform.DOLocalMoveY(GoldenBall.transform.position.y - 1.5f, _animationSpeed ));
            show.AppendCallback(MiddleShowAnimation);
            // show.PrependInterval(1);
            show.Append(transform.DOMoveY(transform.position.y, _animationSpeed));
            show.Join(GoldenBall.transform.DOLocalMoveY(-0.4f, _animationSpeed));
            show.AppendCallback(EndShowAnimation);
            show.Play();
        }

        //moves the cup
        public void Move(Transform point, float yPos)
        {
            float horizontalTossTime = Vector3.Distance(transform.position, point.position) / _horizontalTossSpeed;
            float verticalTossTime = Mathf.Abs(yPos / _verticalTossSpeed);

            DOTween.Sequence()
                .Append(transform.DOMoveY(transform.position.y - yPos, verticalTossTime).SetEase(Ease.Linear))
                .Append(transform.DOMoveX(point.position.x, horizontalTossTime).SetEase(Ease.Linear))
                .Append(transform.DOMoveY(point.position.y, verticalTossTime).SetEase(Ease.Linear))
                .Append(transform.DOMoveZ(point.transform.position.z, 0.1f))
                .AppendCallback(() => OnEndMove?.Invoke());
        }

        private void OnMouseUpAsButton()
        {
            StartShowAnimation();
            MoveShowGoldenBall();
        }
    }
}