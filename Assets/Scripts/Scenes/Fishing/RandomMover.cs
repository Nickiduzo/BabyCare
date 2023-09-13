using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fishing
{
    public class RandomMover : MonoBehaviour
    {
        private const string SpeedKey = "Speed";

        [SerializeField] private bool _rotateInsteadOfFlip = false;

        [SerializeField] private bool _canMoveVertically = false;
        [SerializeField] private bool _canIdle = false;
        [SerializeField] private float _maxMoveSpeed = 4;
        [SerializeField] private float _minMoveSpeed = 2;
        [SerializeField] private float _distance = 10f;
        [SerializeField] private float _screenOffset;
        [SerializeField] private List<SpriteRenderer> _sprites;
        [SerializeField] private float _accelerationValue;

        private float _moveSpeed;
        private float _maxDistance;
        private float _minDistance;
        private float initialYPos;
        private float idleMinDur = 1;
        private float idleMaxDur = 10;
        private float _verticalSpeed = 0.25f;
        
        private bool IsCanMove => _canIdle && !IsIdling || !_canIdle;
        private bool IsIdling { get; set; }
       
        private Animator Animator { get; set; }

        private bool StopOnDestination { get; set; } = false;
        
        private float VerticalSpeed
        {
            get
            {
                CheckVerticalSpeedDirection();
                return _verticalSpeed;
            }
            set
            {
                _verticalSpeed = value;
            }
        }
        
        private void Awake()
            => Init();

        private void Update()
            => Move();
        
        private void CheckVerticalSpeedDirection()
        {
            if (this.transform.position.y >= initialYPos + this.transform.localScale.z && _verticalSpeed > 0)
                _verticalSpeed *= -1;
            if (this.transform.position.y <= initialYPos && _verticalSpeed < 0)
                _verticalSpeed *= -1;
        }

        public void Construct(Animator animator = null)
        {
            Animator = animator;
        }


        private void Init()
        {
            VerticalSpeed = _verticalSpeed;
            initialYPos = this.transform.position.y;
            _moveSpeed = GetRandomMoveSpeed();
            CalculateDistance();
        }

        private void Move()
        {
            if (!IsCanMove) return;

            Vector3 verticalMovement = _canMoveVertically ? transform.up * (VerticalSpeed * Time.deltaTime) : Vector3.zero;
            Vector3 movement = transform.right * (_moveSpeed * Time.deltaTime) + verticalMovement;
            transform.Translate(movement);
            Flip();
            CalculateAcceleration();
        }

        private void CalculateAcceleration()
            => _moveSpeed += _moveSpeed < 0 ? -_accelerationValue : _accelerationValue;

        //Calculating max and min distance of movement
        private void CalculateDistance()
        {
            if (Camera.main == null) return;

            Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            _maxDistance = Mathf.Clamp(transform.position.x + _distance, -screenBounds.x, screenBounds.x + _screenOffset);
            _minDistance = Mathf.Clamp(transform.position.x - _distance, -screenBounds.x, screenBounds.x + _screenOffset);

        }

        private void Idle()
        {
            var Sequence = DOTween.Sequence();
            if (_canIdle)
            {
                Sequence.AppendCallback(() => IsIdling = true);
                Sequence.AppendInterval(Random.Range(idleMinDur, idleMaxDur));
                Sequence.AppendCallback(() => IsIdling = false);
            }
        }

        // Flips fish sprite after changing direction
        private void Flip()
        {
            if (StopOnDestination)
                _moveSpeed = _moveSpeed > 0 ? _maxMoveSpeed * 2 : -_maxMoveSpeed * 2;

            if (transform.position.x >= _maxDistance)
            {
                if (_sprites.Count == 0)
                {
                    float XScale = transform.localScale.x > 0 ? -transform.localScale.x : transform.localScale.x;
                    transform.localScale = new Vector3(XScale, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    if (_rotateInsteadOfFlip)
                    {
                        this.transform.rotation = Quaternion.Euler(Vector3.zero);
                    }
                    else
                    {
                        foreach (SpriteRenderer sprite in _sprites)
                            sprite.flipX = false;
                    }
                }
                _moveSpeed = GetRandomMoveSpeed();
                _verticalSpeed *= -1;
                Idle();
            }

            if (transform.position.x <= _minDistance)
            {
                if (_sprites.Count == 0)
                {
                    float XScale = transform.localScale.x < 0 ? -transform.localScale.x : transform.localScale.x;
                    transform.localScale = new Vector3(XScale, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    if (_rotateInsteadOfFlip)
                    {
                        this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else
                    {
                        foreach (SpriteRenderer sprite in _sprites)
                            sprite.flipX = true;
                    }
                }
                _moveSpeed = GetRandomMoveSpeed() * -1;
                _verticalSpeed *= -1;
                Idle();
            }

        }

        private float GetRandomMoveSpeed()
        {
            var speed = StopOnDestination ? 0 : -Random.Range(_minMoveSpeed, _maxMoveSpeed);
            if (Animator) Animator.SetFloat(SpeedKey, Mathf.Abs(speed));
            return speed;
        }

        internal void StopOnReachingDestination()
        {
            _accelerationValue = 0;
            Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            _maxDistance = screenBounds.x + _screenOffset;
            _minDistance = -screenBounds.x - _screenOffset;
            StopOnDestination = true;
        }
    }
}