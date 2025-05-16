using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class StartGameState : LifecycleState
    {
        public override void Enter()
        {
            base.Enter();
            
            _lifecycleContainer.FieldCatcher.CloseCatcher();
            _lifecycleContainer.SetPointerProvidersEnableStatus(false);
            _lifecycleContainer.GyroscopeGravityChanger.SetIsEnable(false);
            
            StartGame().Forget();
        }

        private async UniTaskVoid StartGame()
        {
            var config = _lifecycleStateMachine.LifecycleStateMachineConfig;

            if (_contextCancellationToken.IsCancellationRequested)
            {
                return;
            }
            
            await UniTask
                .WhenAll(
                    _lifecycleContainer.FieldCatcherSpawner.FillCatcher(_contextCancellationToken),
                    config.TextPopup?.AnimateTexts(config.StartGamePhrases, config.PhrasesDuration)  ?? UniTask.CompletedTask
                );
            
            _lifecycleStateMachine.EnterIn<CoreGameState>();
        }
    }
}