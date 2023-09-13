using DG.Tweening;
using UnityEngine;

public class RayUIRotator : MonoBehaviour
{
    [SerializeField] private float _maxRotateDuration;
    [SerializeField] private float _rotateAngle;

    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.DORotate(new Vector3(0, 0, _rotateAngle), Random.Range(2f, _maxRotateDuration))
            .SetLoops(-1, LoopType.Yoyo)
            .OnComplete(Rotate);
    }
}
