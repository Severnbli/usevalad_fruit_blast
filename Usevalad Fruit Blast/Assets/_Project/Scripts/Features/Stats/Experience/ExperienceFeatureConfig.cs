using System;
using _Project.Scripts.Common.UI.Bars.ProgressBar;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Experience
{
    [Serializable]
    public class ExperienceFeatureConfig : IFeatureConfig
    {
        [SerializeField] private int _startLevel = 1;
        [SerializeField] private int _startLevelMaxExperience = 200;
        [SerializeField] private ExperienceGrowthType _experienceGrowthType;
        [SerializeField] private int _experienceGrowthRate = 50;
        [SerializeField] private ProgressBar[] _progressBars;
        
        public int StartLevel => _startLevel;
        public int StartLevelMaxExperience => _startLevelMaxExperience;
        public ExperienceGrowthType ExperienceGrowthType => _experienceGrowthType;
        public int ExperienceGrowthRate => _experienceGrowthRate;
        public ProgressBar[] ProgressBars => _progressBars;
    }
}