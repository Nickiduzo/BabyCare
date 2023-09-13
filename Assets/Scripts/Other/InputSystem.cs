using System;
using UnityEngine;

namespace Inputs
{
    public class InputSystem : MonoBehaviour, IInputSystem
    {
        public event Action<Vector3> OnTapped;

        private void Update()
        {
            if (Tapped())
            {
                OnTapped?.Invoke(CalculateTouchPosition());
            }
        }

        public Vector2 CalculateTouchPosition()
        {
            Vector2 tapPosition = new Vector3();

#if UNITY_EDITOR

            if (Camera.main != null)
            {
                tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return tapPosition;
            }
            return tapPosition;

#endif
#if UNITY_ANDROID || UNITY_IOS

            tapPosition = CalculateFingerTouchPosition();
            return tapPosition;

#endif
        }

        private bool Tapped()
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
                return true;

            return false;
        }

        private Vector2 CalculateFingerTouchPosition()
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            return touchPosition;
        }
    }
}