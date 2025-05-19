namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class DefeatDialogState : LifecycleState
    {
        public override void Enter()
        {
            base.Enter();
            
            _lifecycleStateMachine.EnterIn<ResetGameState>();
        }
    }
}