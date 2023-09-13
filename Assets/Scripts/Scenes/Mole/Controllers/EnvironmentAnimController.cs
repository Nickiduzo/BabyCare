using DG.Tweening;
using Mole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnimController : MonoBehaviour
{
    [SerializeField] bool _isIdleAnim;
    [Tooltip("Multiplier for shake"), SerializeField] float _rotationIntensity = 10f;
    [Tooltip("Multiplier for shake"), SerializeField] float _positionIntensity = .5f;
    [SerializeField] float _flyOffset = 2f, _maxOffset = 1.5f, _minRandom = .15f, _maxRandom = .3f;
    [SerializeField] float _rotateAmount = 2.5f;
    
    private Sequence _idleSequence;

    void Start()
    {
        IdleAnim();
    }

    private void IdleAnim()
    {
        if (!_isIdleAnim) return;

        _rotateAmount = -_rotateAmount;
        _idleSequence?.Kill();
        _idleSequence = DOTween.Sequence()
            //.Append(transform.DOBlendableScaleBy(new Vector3(0f, Random.Range(.03f, .05f), 0f), 1.5f))
            .Join(transform.DORotate(new Vector3(0f, 0f, _rotateAmount), 1.5f)).SetEase(Ease.InOutQuad)
            .OnComplete(IdleAnim);//.SetLoops(-1, LoopType.Yoyo);
    }

    public void RotateForDuration(float duration)
    {
        DOTween.Sequence()
        .Append(transform.DOShakeRotation(duration, Vector3.forward * _rotationIntensity)).SetEase(Ease.Flash)
        .Join(transform.DOShakePosition(duration, _positionIntensity))
        .OnStart(() => _idleSequence.Pause())
        .OnComplete(() =>
        {
            // Ensure the object ends with the exact rotation as intended
            _idleSequence.Play();
        });
    }
}
