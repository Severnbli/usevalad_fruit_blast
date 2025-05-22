using _Project.Scripts.Common.UI.Popups;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Lifecycle.GameTime.GameTimeProvider;
using _Project.Scripts.Features.Lifecycle.LifecycleStateMachine;
using _Project.Scripts.Features.Lifecycle.LifecycleStateMachine.States;

namespace _Project.Scripts.Features.UI.Screens.PauseScreen
{
    public class PauseScreen : ScreenFeature, IDestroyableFeature
    {
        private LifecycleStateMachine _lifecycleStateMachine;
        private GameTimeProvider _gameTimeProvider;
        private UIProvider.UIProvider _uiProvider;
        
        public PausePopup PausePopup { get; private set; }
        
        public override void Init()
        {
            base.Init();
            
            Context.TryGetComponentFromContainer(out _lifecycleStateMachine);
            Context.TryGetComponentFromContainer(out _gameTimeProvider);

            if (Context.TryGetComponentFromContainer(out _uiProvider))
            {
                PausePopup = _uiProvider.UIProviderConfig.PausePopup;
            }
            
            SetupListeners();
        }

        private void SetupListeners()
        {
            PausePopup.PauseButton.onClick.AddListener(() =>
            {
                _gameTimeProvider.PauseGame();
                _lifecycleStateMachine.LifecycleContainer.SetPointerProvidersEnableStatus(false);
            });
            
            PausePopup.ResumeButton.onClick.AddListener(() =>
            {
                _gameTimeProvider.ResumeGame();
                _lifecycleStateMachine.LifecycleContainer
                    .SetPointerProvidersEnableStatus(_lifecycleStateMachine.LifecycleContainer.UserInputAvailability);
            });
            
            PausePopup.RestartButton.onClick.AddListener(() =>
            {
                _lifecycleStateMachine.EnterIn<EndGameState>();
                _gameTimeProvider.ResumeGame();
                _lifecycleStateMachine.LifecycleContainer
                    .SetPointerProvidersEnableStatus(_lifecycleStateMachine.LifecycleContainer.UserInputAvailability);
            });
            
            _lifecycleStateMachine.LifecycleContainer.OnChangeUserInputAvailability += SetPauseButtonInteractable;
        }

        public void SetPauseButtonInteractable(bool isInteractable)
        {
            PausePopup.PauseButton.interactable = isInteractable;
        }

        public void OnDestroy()
        {
            if (PausePopup != null)
            {
                PausePopup.PauseButton.onClick.RemoveListener(() => _gameTimeProvider.PauseGame());
                PausePopup.ResumeButton.onClick.RemoveListener(() => _gameTimeProvider.ResumeGame());
                PausePopup.RestartButton.onClick.RemoveListener(() => _lifecycleStateMachine.EnterIn<EndGameState>());
            }

            if (_lifecycleStateMachine.LifecycleContainer != null)
            {
                _lifecycleStateMachine.LifecycleContainer.OnChangeUserInputAvailability -= SetPauseButtonInteractable;
            }
        }
    }
}