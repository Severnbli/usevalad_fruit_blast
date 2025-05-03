using _Project.Scripts.Features.Physics.Colliders;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions
{
    public sealed class CollisionPair
    {
        public BaseCollider Collider1 { get; set; }
        public BaseCollider Collider2 { get; set; }
        public Vector2 Normal { get; set; }
        public float Depth { get; set; }
        
        public CollisionPair() {}

        public CollisionPair(BaseCollider bc1, BaseCollider bc2, Vector2 normal, float depth)
        {
            Collider1 = bc1;
            Collider2 = bc2;
            Normal = normal;
            Depth = depth;
        }
    }
}