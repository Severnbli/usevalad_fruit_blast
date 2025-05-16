using System.Collections.Generic;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider
{
    [CreateAssetMenu(fileName = "ExperienceEffectProviderConfig", menuName = "Configs/Effects/Providers/Experience Effect Provider Config")]

    public class ExperienceEffectProviderConfig : EffectProviderConfig
    {
        [SerializeField] private GameObject _experiencePrefab;
        [SerializeField] private List<ExperienceEffectGroup> _experienceEffectObjects;
        
        public GameObject ExperiencePrefab => _experiencePrefab;
        public List<ExperienceEffectGroup> ExperienceEffectObjects => _experienceEffectObjects;
    }
}