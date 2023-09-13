using DG.Tweening;
using Mole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoveController : MonoBehaviour
{
    [SerializeField] Transform _startPoint, _endPoint;
    [SerializeField] float _speed;

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        float distance = Mathf.Abs(_endPoint.position.x - transform.position.x);
        float duration = distance / _speed;

        transform.DOMoveX(_endPoint.position.x, duration).OnComplete(() =>
        {
            transform.position = _startPoint.position;
            Move();
        });
    }
}
