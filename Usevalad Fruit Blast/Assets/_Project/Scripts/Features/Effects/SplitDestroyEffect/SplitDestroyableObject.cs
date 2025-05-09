using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.SplitDestroyEffect
{
    public class SplitDestroyableObject : EffectDestroyableObject
    {
        private SplitDestroyProvider.SplitDestroyProvider _splitDestroyProvider;
        public SpriteRenderer SpriteRenderer { get; private set; }

        private void Start()
        {
            if (!ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                return;
            }

            var context = systemCoordinator.Context;
            
            context.TryGetComponentFromContainer(out _splitDestroyProvider);
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void EffectDestroy()
        {
            _splitDestroyProvider.SplitDestroy(this);
        }
    }
}