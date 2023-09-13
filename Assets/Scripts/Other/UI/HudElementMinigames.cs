using DG.Tweening;
using Sound;
using UnityEngine;

namespace UI
{
    public class HudElementMinigames : MonoBehaviour
    {
        [SerializeField] private float _appearDuration;
        [SerializeField] private Vector3 _scaleEndValue;
        [SerializeField] protected SoundSystem _soundSystem;
        private RectTransform _rectTransform;
        private MiniGameButton[] _allButtons;   // Масив усіх кнопок міні-ігор на сцені

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _allButtons = FindObjectsOfType<MiniGameButton>();  // Знаходимо усі кнопки міні-ігор на сцені
        }

        // Внутрішній метод для зміни масштабу елемента з вказаною тривалістю та ефектом
        private void ScaleTo(Vector3 targetScale, float duration, Ease ease)
        {
            _rectTransform.DOScale(targetScale, duration).SetEase(ease);    // Використовуємо DOTween для зміни масштабу з анімацією
        }

        public void Appear()
        {
            ScaleTo(_scaleEndValue, _appearDuration, Ease.InSine);  // Застосовуємо анімацію появи елемента
        }

        public void Disappear()
        {
            ScaleTo(Vector3.zero, _appearDuration, Ease.Linear);    // Застосовуємо анімацію зникнення елемента (зміна масштабу до нуля)
        }

        public void Click()
        {
            _soundSystem.PlaySound("buttonUIClick");
            _rectTransform.DOShakeScale(0.09f, 0.09f);


            foreach (MiniGameButton button in _allButtons)
            {
                if (button != this)
                {
                    button.Disappear();     // Анімація зникнення всіх кнопок на сцені
                }

            }
        }
    }
}
