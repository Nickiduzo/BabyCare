using DG.Tweening;
using UnityEngine;

public class SunRay : MonoBehaviour
{
    [SerializeField] private Vector3 _scaleValue;
    [SerializeField] private float _minDuration;
    [SerializeField] private float _maxDuration;

    private void Awake()
    {
        Shine();
    }
    private void Shine()
    {
        transform.DOScale(_scaleValue, Random.Range(_minDuration, _maxDuration))
            .SetLoops(-1, LoopType.Yoyo)
            .SetDelay(1f)
            .OnComplete(Shine);
    }
}
