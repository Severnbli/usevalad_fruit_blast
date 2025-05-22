using _Project.Scripts.Features.Effects.Objects.Emitters;

namespace _Project.Scripts.Features.Effects.Providers
{
    public interface IEmitProvider<T> where T : EffectEmitterObject
    {
        public void Emit(T emitterObject);
    }
}