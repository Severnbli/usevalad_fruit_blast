using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class StartGameState : LifecycleState
    {
        // TODO: show start messages and go to core
        
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

            await UniTask
                .WhenAll(
                    _lifecycleContainer.FieldCatcherSpawner.FillCatcher(_contextCancellationToken),
                    config.TextPopup.AnimateTexts(config.StartGamePhrases, config.PhrasesDuration)
                );
                
            
            _lifecycleStateMachine.EnterIn<CoreGameState>();
        }
    }
}