namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine
{
    public abstract class LifecycleState
    {
        protected LifecycleStateMachine _lifecycleStateMachine;
        protected LifecycleContainer _lifecycleContainer;

        public virtual void SetupState(LifecycleStateMachine lifecycleStateMachine)
        {
            _lifecycleStateMachine = lifecycleStateMachine;
            _lifecycleContainer = _lifecycleStateMachine.LifecycleContainer;
        }

        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void Exit() {}
    }
}