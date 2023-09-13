/*namespace Apple.ChainResponsibilityh
{
    public abstract class BaseAction : IChainAction
    {
        private IChainAction _nextAction;

        public IChainAction SetNextAction(IChainAction action)
            => _nextAction = action;


        public virtual void Execute()
        {
            if (_nextAction != null)
                _nextAction.Execute();
        }

    }
}*/