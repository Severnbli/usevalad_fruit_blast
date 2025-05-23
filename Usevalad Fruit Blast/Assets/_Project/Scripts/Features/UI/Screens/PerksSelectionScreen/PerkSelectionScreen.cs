using _Project.Scripts.Common.UI.Bars.LevelBar;
using _Project.Scripts.Common.UI.Popups;
using _Project.Scripts.Features.Bonuses.Perks;
using _Project.Scripts.Features.Bonuses.Perks.PerksProvider;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Lifecycle.GameTime.GameTimeProvider;
using _Project.Scripts.Features.Lifecycle.LifecycleStateMachine;
using _Project.Scripts.Features.Stats.Experience;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Features.UI.Screens.PerksSelectionScreen
{
    public class PerkSelectionScreen : ScreenFeature, IDestroyableFeature
    {
        private ExperienceProvider _experienceProvider;
        private PerksProvider _perksProvider;
        private UIProvider.UIProvider _uiProvider;
        private GameTimeProvider _gameTimeProvider;
        private LifecycleStateMachine _lifecycleStateMachine;
        
        private BasePerk _leftPerk;
        private BasePerk _rightPerk;

        public LevelBar LevelBar { get; private set; }
        public PerkSelectionPopup PerkSelectionPopup { get; private set; }
        
        public override void Init()
        {
            base.Init();
            
            Context.TryGetComponentFromContainer(out _experienceProvider);
            Context.TryGetComponentFromContainer(out _perksProvider);
            Context.TryGetComponentFromContainer(out _uiProvider);
            Context.TryGetComponentFromContainer(out _gameTimeProvider);
            Context.TryGetComponentFromContainer(out _lifecycleStateMachine);

            Setup();
        }

        private void Setup()
        {
            LevelBar = _experienceProvider.ProgressBar.LevelBar;
            PerkSelectionPopup = _uiProvider.UIProviderConfig.PerkSelectionPopup;
            
            PerkSelectionPopup.LeftPopupButton.onClick.AddListener(() =>
            {
                _gameTimeProvider.ResumeGame();
                _lifecycleStateMachine.LifecycleContainer
                    .SetPointerProvidersEnableStatus(_lifecycleStateMachine.LifecycleContainer.UserInputAvailability);
                SelectLeftPerk();
            });
            
            PerkSelectionPopup.RightPopupButton.onClick.AddListener(() =>
            {
                _gameTimeProvider.ResumeGame();
                _lifecycleStateMachine.LifecycleContainer
                    .SetPointerProvidersEnableStatus(_lifecycleStateMachine.LifecycleContainer.UserInputAvailability);
                SelectRightPerk();
            });

            LevelBar.OnLevelUp += CreateSelectionOnLevelUp;
        }

        public void OnDestroy()
        {
            LevelBar.OnLevelUp -= CreateSelectionOnLevelUp;
        }

        private void CreateSelectionOnLevelUp(int level)
        {
            _gameTimeProvider.PauseGame();
            _lifecycleStateMachine.LifecycleContainer.SetPointerProvidersEnableStatus(false);

            PerkSelectionPopup.LevelText.text = level.ToString();
            
            _leftPerk = _perksProvider.GetRandomAvailablePerkByLevel(level);
            PerkSelectionPopup.LeftPopupImage.sprite = _leftPerk.Icon;
            PerkSelectionPopup.LeftPopupText.text = _leftPerk.Name;
            
            _rightPerk = _perksProvider.GetRandomAvailablePerkByLevel(level);
            PerkSelectionPopup.RightPopupImage.sprite = _rightPerk.Icon;
            PerkSelectionPopup.RightPopupText.text = _rightPerk.Name;
            
            PerkSelectionPopup.ScreenPopupSwitcher.Show().Forget();
        }

        private void SelectLeftPerk()
        {
            _perksProvider.Perks.Add(_leftPerk);
        }

        private void SelectRightPerk()
        {
            _perksProvider.Perks.Add(_rightPerk);
        }
    }
}