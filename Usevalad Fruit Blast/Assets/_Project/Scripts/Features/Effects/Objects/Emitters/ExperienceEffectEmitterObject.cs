using _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider;

namespace _Project.Scripts.Features.Effects.Objects.Emitters
{
    public class ExperienceEffectEmitterObject : EffectEmitterObject
    {
        private ExperienceEffectProvider _experienceEffectProvider;

        protected override void Awake()
        {
            base.Awake();

            var context = _systemCoordinator.Context;

            context.TryGetComponentFromContainer(out _experienceEffectProvider);
        }
        
        public override void Emit()
        {
            _experienceEffectProvider?.Emit(this);
        }
    }
}