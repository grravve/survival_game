namespace Assets.Scripts
{
    public abstract class StateMachine
    {
        protected IState _currentState;

        protected abstract void StatesInitialization();

        public abstract void Launch(IState initialState);
        public abstract void ChangeState(IState newState);
    }
}
