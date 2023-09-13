using Sound;
using UnityEngine;

namespace NewFishing
{
    //TODO Rework FixedUpdate
    [RequireComponent(typeof(DragAndDrop))]
    public sealed class RodSound : MonoBehaviour
    {
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private string _tencentSound;
        [SerializeField] private string _relaxSound;
        [SerializeField] private float _tencentSpeed;
        private DragAndDrop _dragAndDrop;
        private Vector3 _oldPosition;
        private bool _moving;

        private void Awake() =>
            _dragAndDrop = GetComponent<DragAndDrop>();

        private bool Tencent =>
            _dragAndDrop.IsDraggable &&
            Vector3.Distance(_oldPosition, transform.position) / Time.deltaTime >= _tencentSpeed;

        private void FixedUpdate()
        {
            if (Tencent)
            {
                if (!_moving)
                {
                    _moving = true;
                    _soundSystem.StopSound(_relaxSound);
                    _soundSystem.PlaySound(_tencentSound);
                }
            }
            else
            {
                if (_moving)
                {
                    _moving = false;
                    _soundSystem.StopSound(_tencentSound);
                    _soundSystem.PlaySound(_relaxSound);
                }
            }

            _oldPosition = transform.position;
        }
    }
}