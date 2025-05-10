using _Project.Scripts.Features.Effects.Objects;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.Lifecycle.Objects;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Destroyers
{
    public abstract class ObjectDestroyer : BaseFeature
    {
        public virtual async UniTaskVoid DestroyObject(GameObject gameObject, float delay)
        {
            await UniTask.WaitForSeconds(delay, cancellationToken: Context.CancellationToken);

            if (gameObject.TryGetComponent(out DestroyEmitterObject destroyEmitterObject))
            {
                destroyEmitterObject.IsActive = true;
            }
            
            Object.Destroy(gameObject);
        }
        
        public abstract void DestroyObjectAt(Vector2 position);
        public abstract bool TryGetNearestObjectThatMatchRules(Vector2 position, out ContainerableObject nearestObject);
    }
}