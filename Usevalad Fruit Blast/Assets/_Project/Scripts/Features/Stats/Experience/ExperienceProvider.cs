using System;
using _Project.Scripts.Common.UI.Bars.ProgressBar;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.UI.UIProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Experience
{
    public class ExperienceProvider : BaseFeature, IConfigurableFeature<ExperienceProviderConfig>, IResettableFeature
    {
        private UIProvider _uiProvider;
        private int _currentLevel;
        private int _currentExperience;
        
        public event Action<int> OnLevelUp;
        public event Action<int> OnLevelDown;

        public ExperienceProviderConfig ExperienceProviderConfig { get; private set; }
        public ProgressBar ProgressBar { get; private set; }
        public Transform ProgressTarget { get; private set; }

        public override void Init()
        {
            base.Init();

            if (Context.TryGetComponentFromContainer(out _uiProvider))
            {
                ProgressBar = _uiProvider.UIProviderConfig.ProgressBar;
                ProgressTarget = _uiProvider.UIProviderConfig.ProgressBar.ProgressTarget; 
            }
        }

        public int CurrentLevel
        {
            get => _currentLevel;
            private set
            {
                _currentLevel = value;

                ProgressBar.SetLevel(value);
            }
        }

        public int CurrentExperience
        {
            get => _currentExperience;
            private set
            {
                _currentExperience = value;

                ProgressBar.SetProgress(value, CurrentLevelMaxExperience);
            }
        }
        
        public int CurrentLevelMaxExperience { get; private set; }
        
        public void Configure(ExperienceProviderConfig experienceProviderConfig)
        {
            ExperienceProviderConfig = experienceProviderConfig;
            
            Reset();
        }
        
        public void Reset()
        {
            CurrentLevel = ExperienceProviderConfig.StartLevel;
            CurrentLevelMaxExperience = ExperienceProviderConfig.StartLevelMaxExperience;
            CurrentExperience = 0;
        }

        public void AddExperience(int amount)
        {
            CurrentExperience += amount;

            while (CurrentExperience >= CurrentLevelMaxExperience)
            {
                CurrentExperience -= CurrentLevelMaxExperience;
                CurrentLevel++;
                OnLevelUp?.Invoke(CurrentLevel);
                UpdateCurrentLevelMaxExperience();
            }
            
            while (CurrentExperience < 0 && CurrentLevel > ExperienceProviderConfig.StartLevel)
            {
                CurrentLevel--;
                UpdateCurrentLevelMaxExperience();
                CurrentExperience += CurrentLevelMaxExperience;
                OnLevelDown?.Invoke(CurrentLevel);
            }

            if (CurrentExperience < 0)
            {
                CurrentExperience = 0;
            }
        }
        
        private void UpdateCurrentLevelMaxExperience()
        {
            var baseValue = ExperienceProviderConfig.StartLevelMaxExperience;
            var levelDelta = ExperienceProviderConfig.StartLevelMaxExperience - CurrentLevel;
            
            if (levelDelta == 0)
            {
                CurrentLevelMaxExperience = baseValue;
                return;
            }
            
            var multiplier = ExperienceProviderConfig.ExperienceGrowthType switch
            {
                ExperienceGrowthType.Linear => levelDelta,
                ExperienceGrowthType.Exponential => Mathf.Pow(levelDelta, 2f),
                ExperienceGrowthType.Logarithmic => Mathf.Log(levelDelta + 1),
                _ => 1f
            };

            CurrentLevelMaxExperience = Mathf.RoundToInt(baseValue + multiplier * ExperienceProviderConfig.ExperienceGrowthRate);
        }

        public bool IsEffectNear(GameObject experienceEffect)
        {
            return Vector2.Distance(experienceEffect.transform.position, ProgressTarget.position) <
                   ExperienceProviderConfig.LiftDistance;
        }
    }
}