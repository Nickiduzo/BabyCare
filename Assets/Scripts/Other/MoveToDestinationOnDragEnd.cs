using DG.Tweening;
using System;
using UnityEngine;

public class MoveToDestinationOnDragEnd : MonoBehaviour
{
    public Action OnMoveComplete;
    [SerializeField] private DragAndDrop _dragAndDrop;
    [SerializeField] private float _duration = 0.5f;
    private Vector3 _destination;
    private Tween _movingToDestination;

    public float Duration => _duration;
    public Tween MovingTween => _movingToDestination;

    public void Construct(Vector3 destination)
    {
        _destination = destination;
        _dragAndDrop.OnDragEnded += MoveToDestination;
    }

    private void OnDestroy() 
        => _dragAndDrop.OnDragEnded -= MoveToDestination;

    public void MoveToDestination()
    {
        _dragAndDrop.IsDraggable = false;
        _movingToDestination =  transform.DOMove(_destination, Duration)
            .OnComplete(() => {
                OnMoveComplete?.Invoke();    
                _dragAndDrop.IsDraggable = true;
            });
    }
}