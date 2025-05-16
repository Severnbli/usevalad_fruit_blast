using System;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Experience
{
    public class ExperienceFeature : BaseFeature, IConfigurableFeature<ExperienceFeatureConfig>, IResettableFeature
    {
        private int _currentLevel;
        private int _currentExperience;

        public event Action<int> OnLevelUp;
        public event Action<int> OnLevelDown;

        public ExperienceFeatureConfig ExperienceFeatureConfig { get; private set; }

        public int CurrentLevel
        {
            get => _currentLevel;
            private set
            {
                _currentLevel = value;

                foreach (var progressBar in ExperienceFeatureConfig.ProgressBars)
                {
                    progressBar.SetLevel(value);
                }
            }
        }

        public int CurrentExperience
        {
            get => _currentExperience;
            private set
            {
                _currentExperience = value;

                foreach (var progressBar in ExperienceFeatureConfig.ProgressBars)
                {
                    progressBar.SetProgress(value, CurrentLevelMaxExperience);
                }
            }
        }
        
        public int CurrentLevelMaxExperience { get; private set; }
        
        public void Configure(ExperienceFeatureConfig experienceFeatureConfig)
        {
            ExperienceFeatureConfig = experienceFeatureConfig;
            
            Reset();
        }
        
        public void Reset()
        {
            CurrentLevel = ExperienceFeatureConfig.StartLevel;
            CurrentLevelMaxExperience = ExperienceFeatureConfig.StartLevelMaxExperience;
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
            
            while (CurrentExperience < 0 && CurrentLevel > ExperienceFeatureConfig.StartLevel)
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
            var baseValue = ExperienceFeatureConfig.StartLevelMaxExperience;
            var levelDelta = ExperienceFeatureConfig.StartLevelMaxExperience - CurrentLevel;
            
            if (levelDelta == 0)
            {
                CurrentLevelMaxExperience = baseValue;
                return;
            }
            
            var multiplier = ExperienceFeatureConfig.ExperienceGrowthType switch
            {
                ExperienceGrowthType.Linear => levelDelta,
                ExperienceGrowthType.Exponential => Mathf.Pow(levelDelta, 2f),
                ExperienceGrowthType.Logarithmic => Mathf.Log(levelDelta + 1),
                _ => 1f
            };

            CurrentLevelMaxExperience = Mathf.RoundToInt(baseValue + multiplier * ExperienceFeatureConfig.ExperienceGrowthRate);
        }
    }
}