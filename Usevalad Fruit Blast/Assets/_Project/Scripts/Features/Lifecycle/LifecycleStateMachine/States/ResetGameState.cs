namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class ResetGameState : LifecycleState
    {
        public override void Enter()
        {
            base.Enter();
            
            _lifecycleContainer.SystemCoordinator.Reset();
            
            _lifecycleStateMachine.EnterIn<StartGameState>();

            _lifecycleContainer.ChangeUserInputAvailability(false);
        }
    }
}