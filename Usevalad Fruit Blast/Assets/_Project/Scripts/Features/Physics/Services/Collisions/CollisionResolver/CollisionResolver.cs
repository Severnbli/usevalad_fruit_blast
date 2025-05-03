using System.Collections.Generic;
using _Project.Scripts.Features.Physics.Colliders;

namespace _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver
{
    public class CollisionResolver
    {
        private readonly CollisionResolverConfig _collisionResolverConfig;

        public CollisionResolver(CollisionResolverConfig collisionResolverConfig)
        {
            _collisionResolverConfig = collisionResolverConfig;
        }

        public void CheckCollisionsAndResolve(List<BaseCollider> colliders)
        {
            
        }
    }
}