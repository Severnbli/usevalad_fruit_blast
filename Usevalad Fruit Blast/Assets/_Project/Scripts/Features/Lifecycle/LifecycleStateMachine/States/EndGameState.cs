using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class EndGameState : LifecycleState
    {
        // TODO: open the catcher, finish all anims
        public override void Enter()
        {
            base.Enter();
            
            _lifecycleContainer.FieldCatcher.OpenCatcher();
            
            _lifecycleContainer.SetPointerProvidersEnableStatus(false);
            _lifecycleContainer.GyroscopeGravityChanger.SetIsEnable(false);
            
            Wait5Seconds().Forget();
        }

        private async UniTask Wait5Seconds()
        {
            await UniTask.WaitForSeconds(5f, cancellationToken: _contextCancellationToken);
            
            _lifecycleStateMachine.EnterIn<ResetGameState>();
        }
    }
}