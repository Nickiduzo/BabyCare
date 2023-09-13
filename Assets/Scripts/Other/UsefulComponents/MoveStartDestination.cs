using DG.Tweening;
using UnityEngine;
using System;

namespace UsefulComponents
{
    public class MoveStartDestination : MonoBehaviour
    {
        [SerializeField] private float _movingDuration = 1f;
        private Vector3 _destination;
        private Vector3 _start;

        public Vector3 Destination => _destination;

        public Action MoveToDestinationCompleted;
        public Action MoveToStartCompleted;

        public void Construct(Vector3 destination, Vector3 start)
        {
            _destination = destination;
            _start = start;
        }

        public Tween MoveToDestination()
            => MoveTo(_destination).OnComplete(()=> 
                {
                    MoveToDestinationCompleted?.Invoke();
                });

        public Tween MoveToStart()
            => MoveTo(_start).OnComplete(()=> 
                {
                    MoveToStartCompleted?.Invoke();
                });

        private Tween MoveTo(Vector3 targetPoint)
            => transform.DOMove(targetPoint, _movingDuration);
    }
}