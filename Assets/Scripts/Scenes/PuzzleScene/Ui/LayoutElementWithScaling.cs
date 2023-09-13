using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LayoutElementWithScaling : MonoBehaviour, ILayoutElement
{
    [SerializeField, Range(1, 5)] private float _activateSize;
    [SerializeField, Range(0, 1)] private float _sizeTime;
    [SerializeField, Range(0, 5)] private float _normalSize;
    [SerializeField, Range(0, 10)] private float _moveTime;
    [SerializeField] private Image _positionPoint;

    public void Activate()
    {
        transform.DOScale(_activateSize, _sizeTime);
        _positionPoint.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        transform.DOScale(_normalSize, _sizeTime);
        _positionPoint.gameObject.SetActive(false);
    }

    public void Translate(Vector3 motion) =>
        transform.DOMove(transform.position + motion, _moveTime);
}