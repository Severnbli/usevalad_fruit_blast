using System;
using _Project.Scripts.Common.UI.Bars.HealthBar;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.UI.UIProvider;

namespace _Project.Scripts.Features.Stats.Health
{
    public class HealthProvider : StatsFeature, IConfigurableFeature<HealthProviderConfig>, IResettableFeature
    {
        private UIProvider _uiProvider;
        private int _currentHealth;
        
        public event Action<int> OnHealthUpdate;
        public HealthProviderConfig HealthProviderConfig { get; private set; }
        public HealthBar HealthBar { get; private set; }

        public override void Init()
        {
            base.Init();

            if (Context.TryGetComponentFromContainer(out _uiProvider))
            {
                HealthBar = _uiProvider.UIProviderConfig.HealthBar;
            }
        }

        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;

                HealthBar.SetHealth(_currentHealth);
            }
        }
        
        public void Configure(HealthProviderConfig healthProviderConfig)
        {
            HealthProviderConfig = healthProviderConfig;
            
            Reset();
        }
        
        public void Reset()
        {
            CurrentHealth = HealthProviderConfig.MaxHealth;
        }
        
        public void DecreaseHealth(int amount)
        {
            CurrentHealth -= amount;
            OnHealthUpdate?.Invoke(_currentHealth);
        }

        public void IncreaseHealth(int amount)
        {
            CurrentHealth += amount;
            OnHealthUpdate?.Invoke(_currentHealth);
        }
    }
}