using DG.Tweening;
using Mole;
using Sound;
using System.Collections;
using TMPro;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] RocketView _rocket;
    [SerializeField] SoundSystem _soundSystem;
    [SerializeField] float _yPosBreak, _tilt = 45f, _deadZone, _rotateSpeed;
    [SerializeField] bool _isValidate = true;

    public bool IsValidate { get { return _isValidate; } set { _isValidate = value; } }

    private bool _isCrashed = false;
    private Sequence _sequence;

    private void Start()
    {
        _sequence = DOTween.Sequence();
        DOTween.SetTweensCapacity(3125, 825);

        _rocket.OnCollisionEnter += OnCrashedEnter;
    }

    private void FixedUpdate()
    {
        if (_isCrashed) return;

        if (Input.GetMouseButton(0) && _isValidate)
        {
            Move(_rocket.InputSystem.CalculateTouchPosition());
        }

        MoveForward();
    }

    private void Move(Vector3 targetPos)
    {
        var target = new Vector3(0f, targetPos.y, 0f);

        // Clamp the target position within the specified borders
        target.y = Mathf.Clamp(target.y, -_yPosBreak, _yPosBreak);

        // Check if the target position is within the dead zone range or distance between positions bigger than threshold
        if (Mathf.Abs(target.y - transform.position.y) <= _deadZone)
        {
            _sequence?.Kill();
            RotateToDefault(_rocket.transform);
            return;
        }

        //Set duration for dotween
        float distance = Mathf.Abs(target.y - transform.position.y);
        float duration = distance / _rocket.SpeedUpDown;

        Vector3 targetRotation = new(0f, 0f, CalculateTilt(targetPos, -_tilt, _tilt));
        float distanceOfRotation = Quaternion.Angle(transform.rotation, Quaternion.Euler(targetRotation));
        float durationOfRotation = distanceOfRotation / _rotateSpeed;

        _sequence?.Kill();
        _sequence = DOTween.Sequence();
        _sequence.SetAutoKill(false);
        //Rotation of the ship
        _sequence.AppendCallback(() =>
        {
            if (transform.position.y >= _yPosBreak || transform.position.y <= -_yPosBreak) //Threshold of chunk
            {
                RotateToDefault(_rocket.transform);
            }
            else //Rotate beteen tilt limits
            {
                _rocket.transform.DORotate(targetRotation, durationOfRotation);
            }
        })
        //Movement to the target point
        .Join(_rocket.transform.DOMoveY(target.y, duration).SetEase(Ease.Linear)).OnUpdate(() =>
        {
            //If tap unhelded
            if(!Input.GetMouseButton(0) || _rocket.transform.position.y == target.y) 
            {
                RotateToDefault(_rocket.transform);
                _sequence?.Kill();
            }
        })
        .AppendCallback(() =>
        {
        if (!Input.GetMouseButton(0)) //In the end of rotation
            _rocket.transform.DORotate(Vector3.zero, .5f);
        })
        .Play();
    }

    private void MoveForward()
    {
        _rocket.transform.DOBlendableMoveBy(Vector2.right * Time.fixedDeltaTime * _rocket.SpeedForward, 0.5f);
    }

    private void OnCrashedEnter(ObstacleView obstacle)
    {
        DOTween.Kill(_rocket.transform);
        _isCrashed = true;
        _sequence?.Kill();
        _rocket.Animator.SetBool("IsExtinguish", true);

        //Decrease speed after crash
        if(_rocket.SpeedForward - _rocket.ValueToDecreaseSpeed >= _rocket.InitSpeedForward)
            _rocket.SpeedForward -= _rocket.ValueToDecreaseSpeed;

        _soundSystem.PlaySound("ObstacleCollect");
        _rocket.transform.DORotate(new Vector3(0f, 0f, 360f), 0.5f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                RotateToDefault(_rocket.transform);
                _isCrashed = false;
                _rocket.Animator.SetBool("IsExtinguish", false);
            });
    }

    private Tween RotateToDefault(Transform obj) 
        => obj.DORotateQuaternion(Quaternion.Euler(Vector3.zero), 2f);

    private float CalculateTilt(Vector3 targetPos, float minTilt, float maxTilt)
    {
        Vector3 direction = targetPos - _rocket.transform.position;

        // Calculate the actual tilt based on the normalized value and tilt range
        float tilt = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (tilt >= 135f) tilt = 180f - tilt;
        else if (tilt <= -135f) tilt = -180f - tilt;

        tilt = Mathf.Clamp(tilt, minTilt, maxTilt);

        return tilt;
    }
}