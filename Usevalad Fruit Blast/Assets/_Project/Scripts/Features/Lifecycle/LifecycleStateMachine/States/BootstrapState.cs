namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class BootstrapState : LifecycleState
    {
        public override void Enter()
        {
            base.Enter();
            
            _lifecycleStateMachine.EnterIn<StartGameState>();
        }
    }
}