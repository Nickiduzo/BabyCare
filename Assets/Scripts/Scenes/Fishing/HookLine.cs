using UnityEngine;

namespace Fishing
{
    public class HookLine : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private Transform _hook;

        private LineRenderer _lineRenderer;

        void Start()
            => _lineRenderer = GetComponent<LineRenderer>();

        void Update() =>
            DrawHookLine();

        //Drawing hook line to imitate 
        private void DrawHookLine()
        {
            _lineRenderer.SetPositions(new[]
            {
                _pivot.position, _hook.position
            });
        }

    }
}