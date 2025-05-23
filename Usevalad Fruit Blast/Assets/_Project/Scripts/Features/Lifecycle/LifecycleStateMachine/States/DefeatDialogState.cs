using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States
{
    public class DefeatDialogState : LifecycleState
    {
        public override void Init()
        {
            base.Init();
            
            _lifecycleContainer.DefeatScreen.DefeatPopup.RestartButton.onClick.AddListener(
                () => _lifecycleStateMachine.EnterIn<ResetGameState>());
        }

        public override void Enter()
        {
            base.Enter();
            
            DefeatProcessing().Forget();
        }

        private async UniTaskVoid DefeatProcessing()
        {
            var config = _lifecycleStateMachine.LifecycleStateMachineConfig;

            await (_lifecycleContainer.UIProvider.UIProviderConfig.TextPopup?
                .AnimateTexts(config.DefeatDialogPhrases, config.DefeatDialogPhrasesDuration) ?? UniTask.CompletedTask);
            
            _lifecycleContainer.DefeatScreen.DefeatPopup.ScreenPopupSwitcher.Show().Forget();
        }
    }
}