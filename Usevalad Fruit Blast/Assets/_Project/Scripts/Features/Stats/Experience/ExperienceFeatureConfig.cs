using System;
using _Project.Scripts.Features.FeatureCore;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Experience
{
    [Serializable]
    public class ExperienceFeatureConfig : IFeatureConfig
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private TextMeshProUGUI _progressPercentageText;
        [SerializeField] private int _startLevel = 1;
        [SerializeField] private int _startLevelMaxExperience = 200;
        [SerializeField] private ExperienceGrowthType _experienceGrowthType;
        [SerializeField] private int _experienceGrowthRate = 50;
        
        public TextMeshProUGUI LevelText => _levelText;
        public TextMeshProUGUI ProgressText => _progressText;
        public TextMeshProUGUI ProgressPercentageText => _progressPercentageText;
        public int StartLevel => _startLevel;
        public int StartLevelMaxExperience => _startLevelMaxExperience;
        public ExperienceGrowthType ExperienceGrowthType => _experienceGrowthType;
        public int ExperienceGrowthRate => _experienceGrowthRate;
    }
}