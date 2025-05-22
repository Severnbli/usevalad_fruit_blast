using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects.Emitters
{
    public abstract class EffectEmitterObject : MonoBehaviour
    {
        protected SystemCoordinator _systemCoordinator;
        
        protected virtual void Awake()
        {
            ObjectFinder.TryFindObjectByType(out _systemCoordinator);
        }
        
        public abstract void Emit();
    }
}