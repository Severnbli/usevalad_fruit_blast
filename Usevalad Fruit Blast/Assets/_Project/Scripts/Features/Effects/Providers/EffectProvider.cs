using _Project.Scripts.Features.Effects.Objects;
using _Project.Scripts.Features.Effects.Objects.EffectObjectsContainer;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.Random;

namespace _Project.Scripts.Features.Effects.Providers
{
    public abstract class EffectProvider : BaseFeature
    {
        protected RandomProvider _randomProvider;
        public EffectObjectsContainer _effectObjectsContainer;

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _randomProvider);
            
            Context.TryGetComponentFromContainer(out _effectObjectsContainer);
        }

        public abstract void Emit(EffectEmitterObject emitterObject);
    }
}