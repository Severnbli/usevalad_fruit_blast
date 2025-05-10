using _Project.Scripts.Features.Effects.Objects;
using _Project.Scripts.Features.Effects.Objects.EffectObjectsContainer;
using _Project.Scripts.Features.FeatureCore;

namespace _Project.Scripts.Features.Effects.Providers
{
    public abstract class EffectProvider : BaseFeature
    {
        public EffectObjectsContainer _effectObjectsContainer;

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _effectObjectsContainer);
        }

        public abstract void Emit(EffectEmitterObject emitterObject);
    }
}