using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.GameTime.GameTimeProvider
{
    public class GameTimeProvider : BaseFeature, IConfigurableFeature<GameTimeProviderConfig>
    {
        public GameTimeProviderConfig GameTimeProviderConfig { get; private set; }

        public void Configure(GameTimeProviderConfig gameTimeProviderConfig)
        {
            GameTimeProviderConfig = gameTimeProviderConfig;

            SetupBaseTime();
        }
        
        public void SetupBaseTime()
        {
            Time.timeScale = GameTimeProviderConfig.BaseTime;
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            SetupBaseTime();
        }
    }
}