namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class EndGameState : LifecycleState
    {
        // TODO: open the catcher, finish all anims
        public override void Enter()
        {
            base.Enter();
            
            _lifecycleContainer.SetPointerProvidersEnableStatus(false);
            _lifecycleContainer.GyroscopeGravityChanger.SetIsEnable(false);
        }
    }
}