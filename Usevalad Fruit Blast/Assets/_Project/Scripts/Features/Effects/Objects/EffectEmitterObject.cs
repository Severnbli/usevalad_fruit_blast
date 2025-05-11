using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Effects.Providers;
using _Project.Scripts.Features.Effects.Providers.SplitSpriteEffectProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects
{
    public class EffectEmitterObject : MonoBehaviour
    {
        [SerializeField] private EffectType _effectType;
        [SerializeField] private bool _isActive = false;
        
        private EffectProvider _effectProvider;
        public bool IsActive { get => _isActive; set => _isActive = value; }

        private void Start()
        {
            if (!ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                return;
            }
            
            var context = systemCoordinator.Context;
            
            switch (_effectType)
            {
                case EffectType.SplitSprite:
                {
                    context.TryGetComponentFromContainer(out SplitSpriteEffectProvider splitSpriteEffectProvider);
                    _effectProvider = splitSpriteEffectProvider;
                    break;
                }
            }
        }

        public void Emit()
        {
            _effectProvider?.Emit(this);
        }
    }
}