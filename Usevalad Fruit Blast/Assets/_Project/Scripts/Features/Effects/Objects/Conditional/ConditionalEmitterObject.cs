using _Project.Scripts.Features.Effects.Objects.Emitters;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects.Conditional
{
    public class ConditionalEmitterObject : MonoBehaviour
    {
        private EffectEmitterObject[] _effectEmitterObjects;

        private void Awake()
        {
            _effectEmitterObjects = gameObject.GetComponents<EffectEmitterObject>();
        }

        public void EmitAll()
        {
            foreach (var effectEmitterObject in _effectEmitterObjects)
            {
                effectEmitterObject.Emit();
            }
        }
    }
}