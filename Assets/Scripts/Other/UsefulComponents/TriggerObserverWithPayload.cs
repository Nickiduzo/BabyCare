using System;
using UnityEngine;

namespace UsefulComponents
{
    public class TriggerObserverWithPayload<T> : MonoBehaviour
    {
        public event Action<T> OnTriggerEnter;
        public event Action<T> OnTriggerExit;
        public event Action<T> OnTriggerStay;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out T component))
                OnTriggerEnter?.Invoke(component);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out T component))
                OnTriggerStay?.Invoke(component);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out T component))
                OnTriggerExit?.Invoke(component);
        }
    }
}