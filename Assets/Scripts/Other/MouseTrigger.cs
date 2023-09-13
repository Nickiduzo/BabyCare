using System;
using UnityEngine;


public class MouseTrigger : MonoBehaviour
{
    public event Action OnDrag;
    public event Action OnDown;
    public event Action OnUp;

    private void OnMouseDrag()
        => OnDrag?.Invoke();

    private void OnMouseDown()
        => OnDown?.Invoke();

    private void OnMouseUp()
        => OnUp?.Invoke();
}