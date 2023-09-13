using DG.Tweening;
using UnityEngine;

namespace Bath
{
    [RequireComponent(typeof(Collider), typeof(SpriteRenderer))]
    public class Bubble : MonoBehaviour
    {
        [SerializeField] private Vector3 _destroyMotion;
        [SerializeField, Range(0f, 5f)] private float _destroyDuration;
        private Transform _spawnPoint;
        private SpriteRenderer _sprite;

        public void Construct(Transform spawnPoint)
        {
            _spawnPoint = spawnPoint;
            _sprite = GetComponent<SpriteRenderer>();
            transform.position = _spawnPoint.position;
        }

        //Play destroy animation
        //TODO create and put this in BubbleView
        public void Destroy()
        {
            transform.DOLocalMove(transform.localPosition + _destroyMotion, _destroyDuration);
            _sprite.DOFade(0, _destroyDuration);
            Destroy(gameObject, _destroyDuration);
        }
    }
}
