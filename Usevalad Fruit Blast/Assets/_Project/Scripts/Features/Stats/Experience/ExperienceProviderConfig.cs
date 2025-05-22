using _Project.Scripts.Common.UI.Bars.ProgressBar;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Experience
{
    [CreateAssetMenu (fileName = "ExperienceProviderConfig", menuName = "Configs/Stats/Experience Provider Config")]
    public class ExperienceProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private int _startLevel = 1;
        [SerializeField] private int _startLevelMaxExperience = 200;
        [SerializeField] private ExperienceGrowthType _experienceGrowthType;
        [SerializeField] private int _experienceGrowthRate = 2;
        [SerializeField] private float _liftDistance = 0.1f;
        
        public int StartLevel => _startLevel;
        public int StartLevelMaxExperience => _startLevelMaxExperience;
        public ExperienceGrowthType ExperienceGrowthType => _experienceGrowthType;
        public int ExperienceGrowthRate => _experienceGrowthRate;
        public float LiftDistance => _liftDistance;
    }
}