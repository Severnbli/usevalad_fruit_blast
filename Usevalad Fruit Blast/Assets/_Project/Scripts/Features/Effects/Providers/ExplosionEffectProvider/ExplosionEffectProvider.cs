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
        }
    }
}