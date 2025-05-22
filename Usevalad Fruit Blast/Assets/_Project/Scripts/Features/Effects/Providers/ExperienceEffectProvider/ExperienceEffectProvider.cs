using System.Linq;
using _Project.Scripts.Common.Objects;
using _Project.Scripts.Features.Effects.Objects.Emitters;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.Stats.Experience;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider
{
    public class ExperienceEffectProvider : EffectProvider, IConfigurableFeature<ExperienceEffectProviderConfig>, 
        IEmitProvider<ExperienceEffectEmitterObject>
    {
        public ExperienceProvider _experienceProvider;
        public ExperienceEffectProviderConfig ExperienceEffectProviderConfig { get; private set; }

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _experienceProvider);
        }

        public void Configure(ExperienceEffectProviderConfig experienceEffectProviderConfig)
        {
            ExperienceEffectProviderConfig = experienceEffectProviderConfig;
        }
        
        public void Emit(ExperienceEffectEmitterObject experienceEffectEmitterObject)
        {
            if (!TryGetRandomExperienceEffectGroup(out var randomExperienceEffectGroup))
            {
                return;
            }
            
            SpawnExperienceEffectObject(randomExperienceEffectGroup, experienceEffectEmitterObject);
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
            var config = ExperienceEffectProviderConfig;
            
            var experienceEffectObject = Object.Instantiate(config.ExperienceEffectPrefab);
            
            experienceEffectObject.transform.position = emitterObject.transform.position;
            _effectObjectsContainer.AddToContainer(experienceEffectObject.gameObject);

            var spriteRenderer = experienceEffectObject.SpriteRenderer;
            
            spriteRenderer.sprite = experienceEffectGroup.Icon;
            spriteRenderer.sortingLayerName = config.EffectSortingLayerName;
            spriteRenderer.sortingOrder = config.EffectSortingLayerOrder;
            
            var randExperience = _randomProvider.Random.Next(experienceEffectGroup.MinExperienceAmount, 
                experienceEffectGroup.MaxExperienceAmount);
            
            experienceEffectObject.Setup(randExperience, _experienceProvider.ProgressTarget, _experienceProvider);

            if (experienceEffectObject.gameObject.TryGetComponent(out BezierMover bezierMover))
            {
                bezierMover.Setup(_experienceProvider.ProgressTarget, _randomProvider.Random);
            }
        }
    }
}