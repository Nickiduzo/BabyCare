using System;
using UnityEngine;

namespace Inputs
{
    public interface IInputSystem
    {
        event Action<Vector3> OnTapped;
        Vector2 CalculateTouchPosition();
    }
}