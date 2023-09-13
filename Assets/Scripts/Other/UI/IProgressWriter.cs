using System;

namespace UI
{
    public interface IProgressWriter
    {
        public event Action OnProgressChanged;
        public int CurrentProgress { get; }
        public int MaxProgress { get; }
    }
}