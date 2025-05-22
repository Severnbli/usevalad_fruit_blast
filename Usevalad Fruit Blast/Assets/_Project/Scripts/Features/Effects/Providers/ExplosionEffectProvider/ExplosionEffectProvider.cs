using _Project.Scripts.Features.Effects.Objects.Emitters;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExplosionEffectProvider
{
    public class ExplosionEffectProvider : EffectProvider, IConfigurableFeature<ExplosionEffectProviderConfig>,
        IEmitProvider<ExplosionEffectEmitterObject>
    {
        public ExplosionEffectProviderConfig ExplosionEffectProviderConfig { get; private set; }
        
        public void Configure(ExplosionEffectProviderConfig explosionEffectProviderConfig)
        {
            ExplosionEffectProviderConfig = explosionEffectProviderConfig;
        }
        
        public void Emit(ExplosionEffectEmitterObject explosionEffectEmitterObject)
        {
            var explosionSystem = Object.Instantiate(explosionEffectEmitterObject.ExplosionParticleSystem,
                explosionEffectEmitterObject.transform.position,
                Quaternion.identity);
            
            _effectObjectsContainer.AddToContainer(explosionSystem.gameObject);
            
            explosionSystem.Play();
            Object.Destroy(explosionSystem.gameObject, 
                explosionSystem.main.duration);
        }
    }
}