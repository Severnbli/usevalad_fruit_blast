using _Project.Scripts.Features.Effects;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.Lifecycle.Objects;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Destroyers
{
    public abstract class ObjectDestroyer : BaseFeature
    {
        public virtual void DestroyObject(GameObject gameObject, float delay)
        {
            if (gameObject.TryGetComponent(out EffectDestroyableObject effectDestroyableObject))
            {
                effectDestroyableObject.EffectDestroy();
            }
            
            
        }
        
        public abstract void DestroyObjectAt(Vector2 position);
        public abstract bool TryGetNearestObjectThatMatchRules(Vector2 position, out ContainerableObject nearestObject);
    }
}