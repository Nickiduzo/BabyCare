using DG.Tweening;
using UnityEngine;

public class SunRaysRotator : MonoBehaviour
{
    [SerializeField] private Transform _sunRaysCointainer;
    [SerializeField] private float _rotateDuration;

    private void Awake()
    {
        RotateRays();
    }

    private void RotateRays()
    {
        _sunRaysCointainer.DORotate(new Vector3(0, 0, 360), _rotateDuration, RotateMode.FastBeyond360)
            .OnComplete(RotateRays);
    }
}
