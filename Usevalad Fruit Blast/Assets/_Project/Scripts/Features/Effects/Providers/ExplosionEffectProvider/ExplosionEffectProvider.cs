using _Project.Scripts.Features.Effects.Objects;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExplosionEffectProvider
{
    public class ExplosionEffectProvider : EffectProvider, IConfigurableFeature<ExplosionEffectProviderConfig>
    {
        public ExplosionEffectProviderConfig ExplosionEffectProviderConfig { get; private set; }
        
        public void Configure(ExplosionEffectProviderConfig explosionEffectProviderConfig)
        {
            ExplosionEffectProviderConfig = explosionEffectProviderConfig;
        }
        
        public override void Emit(EffectEmitterObject emitterObject)
        {
            SpawnExplosion(emitterObject);
        }

        private void SpawnExplosion(EffectEmitterObject emitterObject)
        {
            var config = ExplosionEffectProviderConfig;
            var position = emitterObject.transform.position;
            var count = _randomProvider.Random.Next(config.MinExplosionsCount, config.MaxExplosionsCount + 1);

            for (var i = 0; i < count; i++)
            {
                var sprite = config.ExplosionSprites[_randomProvider.Random.Next(0, config.ExplosionSprites.Length)];

                var obj = UnityEngine.Object.Instantiate(config.ExplosionPrefab, position, Quaternion.identity);
                var effect = obj.GetComponent<ExplosionEffectObject>();

                if (effect != null)
                {
                    effect.Initialize(sprite, config.ExplosionDuration);
                }
                
                _effectObjectsContainer.AddToContainer(obj);
            }
        }
    }
}