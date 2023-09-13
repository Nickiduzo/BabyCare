using System;

namespace UsefulComponents
{
    public interface IProgressCalculator
    {
        event Action OnProgressStep;
        void AddProgress(float deltaTime);

    }
}