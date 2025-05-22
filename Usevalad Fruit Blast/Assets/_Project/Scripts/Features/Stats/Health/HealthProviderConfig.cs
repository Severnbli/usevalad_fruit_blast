using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Health
{
    [CreateAssetMenu(fileName = "HealthProviderConfig", menuName = "Configs/Stats/Health Provider Config")]
    public class HealthProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private int _maxHealth = 15;
        
        public int MaxHealth => _maxHealth;
    }
}