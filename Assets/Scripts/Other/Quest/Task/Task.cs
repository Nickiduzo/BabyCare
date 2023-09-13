using DG.Tweening;
using Scene;
using Sound;
using UnityEngine;
using UsefulComponents;

namespace Quest
{
    public class Task : MonoBehaviour
    {
        [SerializeField] private SceneType _type;
        [SerializeField] private float _hintDelay = 15f;
        [SerializeField] private Transform _itemsTransform;
        [SerializeField] private QuestLevelConfig _config;
        [SerializeField] private MouseTrigger _mouseTrigger;

        private SaveLoadSystem _saveLoad;
        private SceneLoader _sceneLoader;
        private SoundSystem _soundSystem;
        private bool _interactable;

        public SceneType Type => _type;

        // task appears 
        private void Awake()
        {
            _mouseTrigger.OnUp += TaskPicked;
            _interactable = false;
            _itemsTransform.localScale = Vector3.zero;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(1f, 0.7f).SetEase(Ease.OutBack));
            sequence.Append(_itemsTransform.DOScale(1f, 0.5f).SetEase(Ease.OutBack));
            sequence.AppendCallback(() => _interactable = true);
            sequence.Play();
        }

        // show hint on task icon
        public void Construct(SoundSystem soundSystem, SceneLoader sceneLoader, SaveLoadSystem saveLoad)
        {
            _sceneLoader = sceneLoader;
            _saveLoad = saveLoad;
            _soundSystem = soundSystem;
            Invoke(nameof(ShowHint), _hintDelay);
        }

        // task was clicked
        private void TaskPicked()
        {
            if (!_interactable) return;
            CancelInvoke(nameof(ShowHint));
            SetSceneTypeInConfig();
            _saveLoad.Save(this);
            _interactable = false;
            ShowClickFx();
            _sceneLoader.LoadScene(_type);
        }

        // save new scene type in config (is used to ensure that in future shopper picks up the same product that he ordered)
        private void SetSceneTypeInConfig()
            => _config.sceneType = _type;

        private void ShowHint()
            =>HintSystem.Instance.ShowPointerHint(transform.position + Vector3.up + Vector3.right);

        private void HideHint()
        {
            if (HintSystem.Instance != null)
            {
                HintSystem.Instance.HidePointerHint();
            }
        }

        // apeear click FX/SFX and hide hint 
        private void ShowClickFx()
        {
            HideHint();
            _soundSystem.PlaySound("PlaybuttonUIClick");
            Vector3 originalScale = transform.localScale;
            transform.DOScale(transform.localScale.x - 0.1f, 0.1f).OnComplete(() =>
            {
                transform.DOScale(originalScale, 0.1f);
            });
        }

        private void OnDestroy()
        {
            HideHint();
            _mouseTrigger.OnUp -= TaskPicked;
        }
    }
}