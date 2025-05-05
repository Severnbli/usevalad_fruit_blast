using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Lifecycle.Objects;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Destroyers
{
    public abstract class ObjectDestroyer : BaseFeature
    {
        public abstract void DestroyObjectAt(Vector2 position);
        public abstract bool TryGetNearestObjectThatMatchRules(Vector2 position, out ContainerableObject nearestObject);
    }
}