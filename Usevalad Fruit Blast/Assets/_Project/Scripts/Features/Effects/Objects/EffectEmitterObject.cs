using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Effects.Providers;
using _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider;
using _Project.Scripts.Features.Effects.Providers.ExplosionEffectProvider;
using _Project.Scripts.Features.Effects.Providers.SplitSpriteEffectProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects
{
    public class EffectEmitterObject : MonoBehaviour
    {
        [SerializeField] private List<EffectType> _effectType;
        [SerializeField] private bool _isActive = false;
        
        private List<EffectProvider> _effectProviders = new();
        public bool IsActive { get => _isActive; set => _isActive = value; }

        private void Start()
        {
            if (!ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                return;
            }
            
            var context = systemCoordinator.Context;

            foreach (var effectType in _effectType.Where((x, y) => x.GetType() != y.GetType()).ToList())
            {
                switch (effectType)
                {
                    case EffectType.SplitSprite:
                    {
                        context.TryGetComponentFromContainer(out SplitSpriteEffectProvider splitSpriteEffectProvider);
                        _effectProviders.Add(splitSpriteEffectProvider);
                        break;
                    }
                    case EffectType.Experience:
                    {
                        context.TryGetComponentFromContainer(out ExperienceEffectProvider experienceEffectProvider);
                        _effectProviders.Add(experienceEffectProvider);
                        break;
                    }
                    case EffectType.Explosion:
                    {
                        context.TryGetComponentFromContainer(out ExplosionEffectProvider explosionEffectProvider);
                        _effectProviders.Add(explosionEffectProvider);
                        break;
                    }
                }   
            }
        }

        public void Emit()
        {
            foreach (var effectProvider in _effectProviders)
            {
                effectProvider?.Emit(this);
            }
        }
    }
}