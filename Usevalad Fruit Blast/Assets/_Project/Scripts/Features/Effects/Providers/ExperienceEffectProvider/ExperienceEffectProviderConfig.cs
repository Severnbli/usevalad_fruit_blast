using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider
{
    [CreateAssetMenu(fileName = "ExperienceEffectProviderConfig", menuName = "Configs/Effects/Providers/Experience Effect Provider Config")]

    public class ExperienceEffectProviderConfig : EffectProviderConfig
    {
        [SerializeField] private ExperienceEffectObject _experienceEffectPrefab;
        [SerializeField] private List<ExperienceEffectGroup> _experienceEffectObjects;
        
        public ExperienceEffectObject ExperienceEffectPrefab => _experienceEffectPrefab;
        public List<ExperienceEffectGroup> ExperienceEffectObjects => _experienceEffectObjects;
    }
}