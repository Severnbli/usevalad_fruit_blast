using _Project.Scripts.Features.FeatureCore;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Features.Stats.Experience
{
    [CreateAssetMenu (fileName = "ExperienceProviderConfig", menuName = "Configs/Stats/Experience Provider Config")]
    public class ExperienceProviderConfig : ScriptableObject, IFeatureConfig
    {
        
    }
}