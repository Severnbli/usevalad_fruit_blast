using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class EndGameState : LifecycleState
    {
        public override void Enter()
        {
            base.Enter();
            
            _lifecycleContainer.FieldCatcher.OpenCatcher();
            
            _lifecycleContainer.SetPointerProvidersEnableStatus(false);
            _lifecycleContainer.GyroscopeGravityChanger.SetIsEnable(false);
            _lifecycleContainer.ChangeUserInputAvailability(false);

            WaitTillTheEnd().Forget();
        }

        private async UniTaskVoid WaitTillTheEnd()
        {
            while (!_contextCancellationToken.IsCancellationRequested)
            {
                var isZeroContainerableObjects = _lifecycleContainer.ObjectsContainer.ContainerableObjects.Count == 0;
                var isZeroEffectContainerableObjects = _lifecycleContainer.EffectObjectsContainer.EffectObjects.Count == 0;
                
                var isGameEnd = 
                        isZeroContainerableObjects
                        && isZeroEffectContainerableObjects;

                if (isGameEnd)
                {
                    break;
                }
                
                await UniTask.Yield();
            }
            
            await UniTask.WhenAll(
                WaitForProgressBar()
            );

            if (_lifecycleContainer.IsRestarting)
            {
                _lifecycleStateMachine.EnterIn<ResetGameState>();   
            }
            else
            {
                _lifecycleStateMachine.EnterIn<DefeatDialogState>();
            }
        }

        private async UniTask WaitForProgressBar()
        {
            var progressBar = _lifecycleContainer.ExperienceProvider.ProgressBar;
            var noUpdatesDelay = _lifecycleStateMachine.LifecycleStateMachineConfig.NoProgressBarUpdatesDelay;

            var timer = 0f;

            while (timer < noUpdatesDelay)
            {
                await UniTask.Yield();
                timer += Time.deltaTime;

                if (progressBar.IsProgressBarOnUpdate)
                {
                    timer = 0f;
                }
            }
        }
    }
}