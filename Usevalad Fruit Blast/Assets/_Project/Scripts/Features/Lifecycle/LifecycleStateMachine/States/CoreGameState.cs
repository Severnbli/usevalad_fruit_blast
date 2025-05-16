using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class CoreGameState : LifecycleState
    {
        private CancellationTokenSource _coreGameCts;
        private CancellationTokenSource _linkedCoreGameCts;

        public override void Enter()
        {
            base.Enter();
            
            UpdateTokens();
            
            _lifecycleContainer.SetPointerProvidersEnableStatus(true);
            _lifecycleContainer.GyroscopeGravityChanger.SetIsEnable(true);
            
            _lifecycleContainer.FieldCatcherSpawner.ContinuousFillCatcher(_linkedCoreGameCts.Token).Forget();
            
            SwitchStateOnCondition().Forget();
        }

        public override void Exit()
        {
            base.Exit();
            
            _coreGameCts.Cancel();
        }

        private async UniTaskVoid SwitchStateOnCondition()
        {
            var healthProvider = _lifecycleContainer.HealthProvider;

            while (healthProvider.CurrentHealth > _lifecycleStateMachine.LifecycleStateMachineConfig.MinHpAmount
                   && !_contextCancellationToken.IsCancellationRequested)
            {
                await UniTask.NextFrame();
            }
            
            _lifecycleStateMachine.EnterIn<EndGameState>();
        }

        private void UpdateTokens()
        {
            _coreGameCts = new CancellationTokenSource();
            _linkedCoreGameCts = CancellationTokenSource.CreateLinkedTokenSource(_contextCancellationToken, 
                _coreGameCts.Token);
        }
    }
}