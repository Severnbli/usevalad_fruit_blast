using _Project.Scripts.Features.Effects.Objects.Conditional;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.Lifecycle.Objects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Features.Lifecycle.Destroyers
{
    public abstract class ObjectDestroyer : BaseFeature
    {
        public void DestroyObject(GameObject gameObject, float delay)
        {
            if (gameObject.TryGetComponent(out DestroyEmitterObject destroyEmitterObject))
            {
                destroyEmitterObject.IsActive = true;
            }

            Object.Destroy(gameObject, delay);
        }
        
        public abstract void DestroyObjectAt(Vector2 position);
        public abstract bool TryGetNearestObjectThatMatchRules(Vector2 position, out ContainerableObject nearestObject);
    }
}