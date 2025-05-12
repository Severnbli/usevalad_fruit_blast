namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class CoreGameState : LifecycleState
    {
        // TODO: filling the catcher until hp > 0 
        
        public override void Enter()
        {
            _lifecycleContainer.SetPointerProvidersEnableStatus(true);
            _lifecycleContainer.GyroscopeGravityChanger.SetIsEnable(true);
        }
    }
}