using System;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;

namespace _Project.Scripts.Features.Stats.Health
{
    public class HealthProvider : StatsFeature, IConfigurableFeature<HealthProviderConfig>, IResettableFeature
    {
        private int _currentHealth;
        
        public event Action<int> OnHealthUpdate;
        public HealthProviderConfig HealthProviderConfig { get; private set; }
        
        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;

                foreach (var healthBar in HealthProviderConfig.HealthBars)
                {
                    healthBar.SetHealth(_currentHealth);
                }
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