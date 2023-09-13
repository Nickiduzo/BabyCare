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
        private MiniGameButton[] _allButtons;   // ����� ��� ������ ��-���� �� ����

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _allButtons = FindObjectsOfType<MiniGameButton>();  // ��������� �� ������ ��-���� �� ����
        }

        // �������� ����� ��� ���� �������� �������� � �������� ��������� �� �������
        private void ScaleTo(Vector3 targetScale, float duration, Ease ease)
        {
            _rectTransform.DOScale(targetScale, duration).SetEase(ease);    // ������������� DOTween ��� ���� �������� � ��������
        }

        public void Appear()
        {
            ScaleTo(_scaleEndValue, _appearDuration, Ease.InSine);  // ����������� ������� ����� ��������
        }

        public void Disappear()
        {
            ScaleTo(Vector3.zero, _appearDuration, Ease.Linear);    // ����������� ������� ��������� �������� (���� �������� �� ����)
        }

        public void Click()
        {
            _soundSystem.PlaySound("buttonUIClick");
            _rectTransform.DOShakeScale(0.09f, 0.09f);


            foreach (MiniGameButton button in _allButtons)
            {
                if (button != this)
                {
                    button.Disappear();     // ������� ��������� ��� ������ �� ����
                }

            }
        }
    }
}
