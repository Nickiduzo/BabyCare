using DG.Tweening;
using UnityEngine;

public class RotateOnDrag : MonoBehaviour
{
    [SerializeField] private MouseTrigger _mouseTrigger;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private float _rotateZAngle;
    private Quaternion _cachedRotation;

    private void Awake()
    {
        _cachedRotation = transform.rotation;
        _mouseTrigger.OnDown += RotateDrag;
        _mouseTrigger.OnUp += RotateToStart;
    }

    private void OnDestroy()
    {
        _mouseTrigger.OnDown -= RotateDrag;
        _mouseTrigger.OnUp -= RotateToStart;
    }

    private void RotateDrag()
    {
        transform.DORotate(new Vector3(0, 0, _rotateZAngle), _rotateDuration);
    }
    
    private void RotateToStart()
    {
        transform.DORotateQuaternion(_cachedRotation, _rotateDuration);
    }
}