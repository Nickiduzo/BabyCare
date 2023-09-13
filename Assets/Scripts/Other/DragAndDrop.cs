using Inputs;
using System;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public bool IsDraggable;
    public event Action OnDragEnded;
    public event Action OnDragStart;
    public event Action OnDrag;

    [SerializeField] private MouseTrigger _mouseTrigger;
    private InputSystem _inputSystem;
    private Vector2 _clampedYPosition;

    public void Construct(InputSystem inputSystem, Vector2 clampedYPosition = default(Vector2))
    {
        _inputSystem = inputSystem;
        _clampedYPosition = clampedYPosition;
    }


    private void Awake()
    {
        _mouseTrigger.OnDrag += CalculateDrag;
        _mouseTrigger.OnDrag += Draging;
        _mouseTrigger.OnDown += DragStart;
        _mouseTrigger.OnUp += DragEnd;
    }

    private void OnDestroy()
    {
        _mouseTrigger.OnDrag -= CalculateDrag;
        _mouseTrigger.OnDrag -= Draging;
        _mouseTrigger.OnDown -= DragStart;
        _mouseTrigger.OnUp -= DragEnd;
    }
    private void CalculateDrag()
    {
        if (!IsDraggable) return;
        Vector3 newPosition = _inputSystem.CalculateTouchPosition();
        if (_clampedYPosition != default(Vector2))
        {
            newPosition.y = Mathf.Clamp(newPosition.y, _clampedYPosition.x, _clampedYPosition.y);
        }

        transform.position = newPosition;

    }

    private void DragEnd()
    {
        if (!IsDraggable) return;

        OnDragEnded?.Invoke();
    }

    private void DragStart()
    {
        if (!IsDraggable) return;
    
        OnDragStart?.Invoke();
    }

    private void Draging()
    {
        OnDrag?.Invoke(); 
    }

}