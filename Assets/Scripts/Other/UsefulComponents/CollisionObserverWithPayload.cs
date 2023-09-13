using System;
using UnityEngine;


public class CollisionObserverWithPayload<T> : MonoBehaviour
{
    public event Action<T> OnCollisionEnter;
    public event Action<T> OnCollisionExit;
    public event Action<T> OnCollisionStay;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T component))
            OnCollisionEnter?.Invoke(component);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T component))
            OnCollisionStay?.Invoke(component);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T component))
            OnCollisionExit?.Invoke(component);
    }
}