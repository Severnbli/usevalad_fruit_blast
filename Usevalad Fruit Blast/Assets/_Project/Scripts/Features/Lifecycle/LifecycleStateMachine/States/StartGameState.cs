namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class StartGameState : LifecycleState
    {
        // TODO: fill the catcher, show start messages and go to core
        
        public override void Enter()
        {
            base.Enter();
            
            _lifecycleContainer.SetPointerProvidersEnableStatus(false);
            _lifecycleContainer.GyroscopeGravityChanger.SetIsEnable(false);
        }
    }
}