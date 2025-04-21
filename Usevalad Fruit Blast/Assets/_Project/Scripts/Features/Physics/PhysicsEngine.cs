using System.Collections.Generic;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Forces;
using _Project.Scripts.Features.Physics.Services.Collisions;
using UnityEngine;

namespace _Project.Scripts.Features.Physics
{
    public class PhysicsEngine : BaseFeature
    { 
        private readonly CollisionResolver _collisionResolver = new();
        
        public List<BaseCollider> Colliders { get; } = new();
        public List<DynamicBody> DynamicBodies { get; } = new();
        public List<ForceProvider> ForceProviders { get; } = new();
        
        public void FixedUpdate()
        {
            ApplyForces();
            ResolveCollisions();
            MoveEntities();
        }

        private void ApplyForces()
        {
            foreach (var dynamicBody in DynamicBodies)
            {
                foreach (var forceProvider in ForceProviders)
                {
                    dynamicBody.ApplyForce(forceProvider.GetForce() * Time.deltaTime);
                }
            }
        }

        private void ResolveCollisions()
        {
            for (int i = 0; i < Colliders.Count - 1; i++)
            {
                for (int j = i + 1; j < Colliders.Count; j++)
                {
                    Colliders[i].ResolveCollision(Colliders[j], _collisionResolver);
                }
            }
        }

        private void MoveEntities()
        {
            foreach (var dynamicBody in DynamicBodies)
            {
                if (dynamicBody.IsStatic)
                {
                    dynamicBody.Velocity = Vector3.zero;
                    continue;
                }
                
                dynamicBody.transform.Translate(dynamicBody.Velocity * Time.deltaTime);
            }
        }

        public override void Init(IFeatureConfig config) {}
    }
}