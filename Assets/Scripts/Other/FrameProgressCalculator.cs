using System;

namespace UsefulComponents
{
    public class FrameProgressCalculator : IProgressCalculator
    {
        private float _currentProgress;
        private float _maxProgress;

        public event Action OnProgressStep;

        // set [_maxProgress] for some action
        public FrameProgressCalculator(float maxProgress)
            => _maxProgress = maxProgress;

        // update progres based on [deltaTime]
        public void AddProgress(float deltaTime)
        {
            _currentProgress += deltaTime;

            if (!IsFullProgress()) return;

            OnProgressStep?.Invoke();
            _currentProgress = 0;
        }

        // check if current progress is max, if so, return "true"
        private bool IsFullProgress()
            => _currentProgress >= _maxProgress;
    }
}