using DG.Tweening;
using UnityEngine;
using Sound;

namespace UsefulComponents
{
    public class HintSystem : MonoBehaviour
    {
        [SerializeField] private Sprite _clickSprite;
        [SerializeField] private Sprite _idleSprite;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private string _clickSound;

        private SpriteRenderer _spriteRenderer;
        private Sequence _pointerSequence;

        #region Singleton
        public static HintSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
            _spriteRenderer = GetComponent<SpriteRenderer>();
            HidePointerHint();
        }
        #endregion Singleton

        public void ShowPointerHint(Vector3 start, Vector3? end = null, float pointerScale = 1f)
        {
            if (_pointerSequence != null)
            {
                DisableAnimation();
            }

            Vector3 currentScale = new Vector3(pointerScale, pointerScale, pointerScale);
            Vector3 scale = currentScale * 0.9f;
            transform.position = start;
            transform.localScale = currentScale;
            _spriteRenderer.enabled = true;
            _pointerSequence = DOTween.Sequence();

            if (end.HasValue)
            {
                _pointerSequence.AppendInterval(0.3f);
                _pointerSequence.AppendCallback(ChangeSprite);
                _pointerSequence.AppendCallback(() => _soundSystem.PlaySound(_clickSound));
                _pointerSequence.Join(transform.DOScale(scale, 0.1f)).SetEase(Ease.Linear);
                _pointerSequence.AppendInterval(0.2f);
                _pointerSequence.Append(transform.DOMove(end.Value, 1f));
                _pointerSequence.AppendInterval(0.2f);
                _pointerSequence.AppendCallback(ChangeSprite);
                _pointerSequence.Append(transform.DOScale(currentScale, 0.3f)).SetEase(Ease.Linear);
                _pointerSequence.AppendInterval(0.2f);
            }
            else
            {
                _pointerSequence.Append(transform.DOScale(scale, 0.2f)).SetEase(Ease.Linear);
                _pointerSequence.AppendCallback(ChangeSprite);
                _pointerSequence.AppendInterval(0.3f);
                _pointerSequence.AppendCallback(ChangeSprite);
                _pointerSequence.Append(transform.DOScale(currentScale, 0.3f)).SetEase(Ease.Linear);

            }

            _pointerSequence.SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
            _pointerSequence.Play();

        }
        public void HidePointerHint()
        {
            DisableAnimation();
            transform.localScale = Vector3.one;
            _spriteRenderer.enabled = false;
        }

        private void ChangeSprite()
            => _spriteRenderer.sprite = (_spriteRenderer.sprite == _clickSprite) ? _idleSprite : _clickSprite;

        private void DisableAnimation()
        {
            _pointerSequence?.Kill();
            if (_spriteRenderer != null)
                _spriteRenderer.sprite = _idleSprite;
        }
    }
}