using System.Collections;
using UnityEngine;

namespace UI
{
    // Uses to merge UI and logic
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private Banner _banner;
        [SerializeField] private Panel _winPanel;
        [SerializeField] protected Hud _hud;
        [SerializeField] private FadeScreenPanel _fadeScreenPanel;
        [SerializeField] private ParticleSystem _finishParticle;

        public Hud Hud => _hud;

        protected IProgressWriter _progressWriter;
        private IWinInvoker _winInvoker;

        private void Awake()
        {
            _fadeScreenPanel.FadingIn += HideProgressBar;
        }
        public virtual void InitProgressBar(IProgressWriter progressWriter)
        {
            _progressWriter = progressWriter;
            ShowProgressBar();
            _hud.SetProgressBarMaxValue(_progressWriter.MaxProgress);
            _progressWriter.OnProgressChanged += UpdateProgressBar;
        }

        public void ResetProgressBar()
        {
            _hud.ResetProgressBar();
        }

        public void HideProgressBar()
            => _hud.HideProgressBar();

        public void ShowProgressBar()
            => _hud.AppearProgressBar();

        public void InitWinInvoker(IWinInvoker winInvoker)
        {
            _winInvoker = winInvoker;
            _winInvoker.OnWin += AppearWinPanel;
        }

        private void AppearWinPanel()
        {
            StartCoroutine(WinSequence());
        }

        private IEnumerator WinSequence()
        {
            //_finishParticle.Play();

            //if(_winPanel is WinPannel winPanel)
            //    winPanel.PlayStarSoundFx();

            _banner.SetScreenSpaceOverlay();
            yield return new WaitForSecondsRealtime(2f);
            _hud.Disappear();
            _winPanel.Appear();
        }

        protected virtual void OnDestroy()
        {
            if (_progressWriter != null)
                _progressWriter.OnProgressChanged -= UpdateProgressBar;
            if (_winInvoker != null)
                _winInvoker.OnWin -= AppearWinPanel;
        }
        private void UpdateProgressBar()
            => _hud.UpdateProgressBar(_progressWriter.CurrentProgress);
    }
}