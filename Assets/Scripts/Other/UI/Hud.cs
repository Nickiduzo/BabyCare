using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private ExitButton _exitButton;
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private Counter _counter;
        [SerializeField] private TimerView _timer;

        private void Awake()
            => Appear();

        private void Appear()
        {
            _exitButton.Appear();
        }

        public virtual void Disappear()
        {
            _exitButton.Disappear();

            if(_progressBar != null)
                _progressBar.Disappear();
        }

        public void SetProgressBarMaxValue(int value)
        {
            _progressBar.SetMaxValue(value);
        }

        public void AppearProgressBar()
        {
            _progressBar.Appear();
        }

        public void UpdateProgressBar(int value)
        {

            _progressBar.SetCurrentValue(value);
        }

        public void HideProgressBar()
        {
            _progressBar.Disappear();
        }

        public void ResetProgressBar()
        {
            _progressBar.SetCurrentValue(0);
            _progressBar.ResetSeparators();
        }

        public void AddScore(ICollectable item)
            => _counter.Score += item.ScorePoints;

        public void DecreaseTime(int time) => _timer.RemainingTime -= time;
        public void IncreaseTime(int time) => _timer.RemainingTime += time;
        public void SetTime(int time) => _timer.RemainingTime = time;
    }
}