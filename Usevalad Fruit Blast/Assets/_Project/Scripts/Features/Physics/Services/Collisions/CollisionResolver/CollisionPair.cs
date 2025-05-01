using _Project.Scripts.Features.Physics.Colliders;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver
{
    public sealed class CollisionPair
    {
        public BaseCollider Collider1 { get; private set; }
        public BaseCollider Collider2 { get; private set; }
        public Vector2 Normal { get; private set; }
        public float Depth { get; private set; }

        public CollisionPair(BaseCollider collider1, BaseCollider collider2, Vector2 normal, float depth)
        {
            Collider1 = collider1;
            Collider2 = collider2;
            Normal = normal;
            Depth = depth;
        }
    }
}