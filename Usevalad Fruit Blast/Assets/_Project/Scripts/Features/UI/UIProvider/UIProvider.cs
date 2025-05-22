using _Project.Scripts.Features.FeatureCore.FeatureContracts;

namespace _Project.Scripts.Features.UI.UIProvider
{
    public class UIProvider : UIFeature, IConfigurableFeature<UIProviderConfig>
    {
        public UIProviderConfig UIProviderConfig { get; private set; }
        
        public void Configure(UIProviderConfig uiProviderConfig)
        {
            UIProviderConfig = uiProviderConfig;
        }
    }
}