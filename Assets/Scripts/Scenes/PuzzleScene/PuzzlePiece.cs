using System.Collections;
using DG.Tweening;
using Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Transform _finalPosition;
    [SerializeField] private float _animationDuration;
    [SerializeField] private SoundSystem _soundSystem;
    [SerializeField] private string _startDragSound;
    [SerializeField] private string _completeSound;

    private Vector3 _defaultPoint;
    private IEnumerator _coroutine;
    private Image _image;
    public bool IsCollected { get; private set; }

    private void Awake()
    {
        _image = GetComponent<Image>();

        _image = _finalPosition.transform.GetComponent<Image>();
        _image.color = new Color(255, 255, 255, 0);
    }

    private void OnEnable()
    {
        _defaultPoint = transform.position;
        IsCollected = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsCollected) return;

        _soundSystem.PlaySound(_startDragSound);
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsCollected) return;
        //transform.position = Input.mousePosition;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

        HilightIfNear();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsCollected) return;

        if (IsDragged()) return;

        ReturnToDefault();
    }

    private void ReturnToDefault()
    {
        if (_coroutine != null) return;
        _coroutine = TransitionToPoint(_defaultPoint);
        StartCoroutine(_coroutine);
    }

    private void HilightIfNear()
    {
        if (Vector3.Distance(transform.position, _finalPosition.position) < 1)
        {
            _image.DOFade(0.5f, 0.1f);
            return;
        }

        _image.DOFade(0, 0.1f);
    }

    private bool IsDragged()
    {
        if (_coroutine != null) return false;

        if (Vector3.Distance(transform.position, _finalPosition.position) > 1) return false;

        IsCollected = true;
        _image.color = new Color(255, 255, 255, 0);
        _coroutine = TransitionToPoint(_finalPosition.position);
        StartCoroutine(_coroutine);
        _soundSystem.PlaySound(_completeSound);

        return true;
    }

    IEnumerator TransitionToPoint(Vector3 endPoint)
    {
        float time = 0;
        Vector3 startPoint = transform.position;

        while (time < _animationDuration)
        {
            Vector3 newPos = Vector3.Lerp(startPoint, endPoint, time / _animationDuration);
            transform.position = newPos;

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = endPoint;

        if (IsCollected)
        {
            PuzzleParticleController.Instance.PlaySuccess(transform.position);
        }

        _coroutine = null;
        yield break;
    }
}