using _Project.Scripts.Features.Effects.Providers.ExplosionEffectProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects.Emitters
{
    public class ExplosionEffectEmitterObject : EffectEmitterObject
    {
        [SerializeField] private ParticleSystem _explosionParticleSystem;
        
        private ExplosionEffectProvider _explosionEffectProvider;

        public ParticleSystem ExplosionParticleSystem => _explosionParticleSystem;
        
        protected override void Awake()
        {
            base.Awake();

            var context = _systemCoordinator.Context;

            context.TryGetComponentFromContainer(out _explosionEffectProvider);
        }

        public override void Emit()
        {
            _explosionEffectProvider?.Emit(this);
        }
    }
}