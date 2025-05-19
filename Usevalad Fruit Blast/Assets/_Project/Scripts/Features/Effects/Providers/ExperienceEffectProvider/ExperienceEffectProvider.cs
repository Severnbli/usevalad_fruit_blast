using System.Linq;
using _Project.Scripts.Features.Effects.Objects;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider
{
    public class ExperienceEffectProvider : EffectProvider, IConfigurableFeature<ExperienceEffectProviderConfig>
    {
        public ExperienceEffectProviderConfig ExperienceEffectProviderConfig { get; private set; }
        
        public void Configure(ExperienceEffectProviderConfig experienceEffectProviderConfig)
        {
            ExperienceEffectProviderConfig = experienceEffectProviderConfig;
        }
        
        public override void Emit(EffectEmitterObject emitterObject)
        {
            if (!TryGetRandomExperienceEffectGroup(out var randomExperienceEffectGroup))
            {
                return;
            }
            
            SpawnExperienceEffectObject(randomExperienceEffectGroup, emitterObject);
        }

        private bool TryGetRandomExperienceEffectGroup(out ExperienceEffectGroup experienceEffectGroup)
        {
            experienceEffectGroup = null;
            
            var activeExperiencesGroups = ExperienceEffectProviderConfig
                .ExperienceEffectObjects.Where(x => x.IsActive).ToList();

            if (activeExperiencesGroups.Count == 0)
            {
                return false;
            }

            var totalProbability = activeExperiencesGroups.Sum(x => x.Probability);
            
            var randProbability = _randomProvider.Random.NextDouble() * totalProbability;

            var currentProbability = 0f;
            
            foreach (var activeExperienceGroup in activeExperiencesGroups)
            {
                currentProbability += activeExperienceGroup.Probability;

                if (randProbability > currentProbability)
                {
                    continue;
                }
                
                experienceEffectGroup = activeExperienceGroup;
                return true;
            }

            return false;
        }

        private void SpawnExperienceEffectObject(ExperienceEffectGroup experienceEffectGroup, EffectEmitterObject emitterObject)
        {
            var experienceEffectObject = Object.Instantiate(ExperienceEffectProviderConfig.ExperiencePrefab);
            
            experienceEffectObject.transform.position = emitterObject.transform.position;
            _effectObjectsContainer.AddToContainer(experienceEffectObject);

            if (experienceEffectObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sprite = experienceEffectGroup.Icon;
                spriteRenderer.sortingLayerName = ExperienceEffectProviderConfig.EffectSortingLayerName;
                spriteRenderer.sortingOrder = ExperienceEffectProviderConfig.EffectSortingLayerOrder;
            }

            if (experienceEffectObject.TryGetComponent(out ExperienceEffectObject experienceEffect))
            {
                experienceEffect.Experience = _randomProvider.Random.Next(experienceEffectGroup.MaxExperienceAmount, experienceEffectGroup.MaxExperienceAmount);
            }
        }
    }
}