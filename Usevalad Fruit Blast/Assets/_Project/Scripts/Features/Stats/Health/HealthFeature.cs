using System;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;

namespace _Project.Scripts.Features.Stats.Health
{
    public class HealthFeature : StatsFeature, IConfigurableFeature<HealthFeatureConfig>, IResettableFeature
    {
        private int _currentHealth;
        
        public event Action<int> OnHealthUpdate;
        public HealthFeatureConfig HealthFeatureConfig { get; private set; }
        
        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value;
                HealthFeatureConfig.HealthText.text = _currentHealth.ToString();
            }
        }
        
        public void Configure(HealthFeatureConfig healthFeatureConfig)
        {
            HealthFeatureConfig = healthFeatureConfig;
            
            Reset();
        }
        
        public void Reset()
        {
            _currentHealth = HealthFeatureConfig.MaxHealth;
            HealthFeatureConfig.HealthText.text = _currentHealth.ToString();
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