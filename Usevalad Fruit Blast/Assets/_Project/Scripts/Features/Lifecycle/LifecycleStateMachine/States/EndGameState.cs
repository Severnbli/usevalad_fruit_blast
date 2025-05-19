using Cysharp.Threading.Tasks;

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
            
            _lifecycleStateMachine.EnterIn<DefeatDialogState>();
        }
    }
}