using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Effects.Providers.SplitSpriteEffectProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects.Emitters
{
    public class SplitSpriteEffectEmitterObject : EffectEmitterObject
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private SplitSpriteEffectProvider _splitSpriteEffectProvider;
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        protected override void Awake()
        {
            base.Awake();

            var context = _systemCoordinator.Context;
            
            context.TryGetComponentFromContainer(out _splitSpriteEffectProvider);
        }
        
        public override void Emit()
        {
            _splitSpriteEffectProvider?.Emit(this);
        }
    }
}