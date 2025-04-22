using System.Collections.Generic;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Engine.Config;
using _Project.Scripts.Features.Physics.Forces;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Engine
{
    public class PhysicsEngine : BaseFeature
    {
        [SerializeField] private float _minimalBodySpeed = 0.1f;
        private CollisionResolver _collisionResolver;
        
        public List<BaseCollider> Colliders { get; } = new();
        public List<DynamicBody> DynamicBodies { get; } = new();
        public List<ForceProvider> ForceProviders { get; } = new();
        
        public void FixedUpdate()
        {
            MoveEntities();
            ResolveCollisions();
            ApplyForces();
        }

        private void ApplyForces()
        {
            foreach (var dynamicBody in DynamicBodies)
            {
                foreach (var forceProvider in ForceProviders)
                {
                    dynamicBody.ApplyForce(forceProvider.GetForce() * Time.fixedDeltaTime);
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
                if (dynamicBody.IsStatic || dynamicBody.Velocity.magnitude < _minimalBodySpeed)
                {
                    dynamicBody.Velocity = Vector3.zero;
                    continue;
                }
                
                dynamicBody.transform.Translate(dynamicBody.Velocity * Time.fixedDeltaTime);
            }
        }

        public override void Init(IFeatureConfig config)
        {
            if (config is not PhysicsEngineConfig physicsEngineConfig)
            {
                return;
            }
            
            _collisionResolver = new(physicsEngineConfig.CollisionResolverConfig);
            
            _minimalBodySpeed = physicsEngineConfig.MinimalBodySpeed;
        }
    }
}