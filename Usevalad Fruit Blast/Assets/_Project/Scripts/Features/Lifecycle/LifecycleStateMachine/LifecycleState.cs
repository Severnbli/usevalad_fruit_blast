using System.Threading;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine
{
    public abstract class LifecycleState
    {
        protected LifecycleStateMachine _lifecycleStateMachine;
        protected LifecycleContainer _lifecycleContainer;
        protected CancellationToken _contextCancellationToken;

        public virtual void SetupState(LifecycleStateMachine lifecycleStateMachine)
        {
            _lifecycleStateMachine = lifecycleStateMachine;
            _lifecycleContainer = _lifecycleStateMachine.LifecycleContainer;
            _contextCancellationToken = lifecycleStateMachine.Context.CancellationToken;
            
            Init();
        }
        
        public virtual void Init() {}
        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void Exit() {}
    }
}