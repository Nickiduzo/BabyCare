using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface ITouchSystem
    {
        event Action<Vector3> OnTapped;
        Vector2 CalculateTouchPosition();
    }