using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Health.HealthInfluencers
{
    [CreateAssetMenu(fileName = "HealthInfluencerConfig", menuName = "Configs/Stats/Health Influencer")]
    public class HealthInfluencerConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private int _healthInfluenceAmount;
        
        public int HealthInfluenceAmount => _healthInfluenceAmount;
    }
}