using _Project.Scripts.Common.UI.Popups;

namespace _Project.Scripts.Features.UI.Screens.DefeatScreen
{
    public class DefeatScreen : ScreenFeature
    {
        private UIProvider.UIProvider _uiProvider;
        
        public DefeatPopup DefeatPopup { get; private set; }
        
        public override void Init()
        {
            base.Init();

            if (Context.TryGetComponentFromContainer(out _uiProvider))
            {
                DefeatPopup = _uiProvider.UIProviderConfig.DefeatPopup;
            }
        }
    }
}