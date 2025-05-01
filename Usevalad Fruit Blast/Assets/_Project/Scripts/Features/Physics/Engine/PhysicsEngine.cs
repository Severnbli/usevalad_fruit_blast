using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public HashSet<BaseCollider> Colliders = new();
        public HashSet<DynamicBody> DynamicBodies = new();
        public HashSet<ForceProvider> ForceProviders = new();
        
        public void FixedUpdate()
        {
            MoveEntities();
            ResolveCollisions();
            ApplyForces();
        }

        private void ResolveCollisions()
        {
            var colliders = Colliders.ToList();
            
            for (var i = 0; i < colliders.Count - 1; i++)
            {
                for (var j = i + 1; j < colliders.Count; j++)
                {
                    colliders[i].ResolveCollision(colliders[j], _collisionResolver);
                }
            }
        }

        private void ApplyForces()
        {
            foreach (var dynamicBody in DynamicBodies)
            {
                foreach (var forceProvider in ForceProviders)
                {
                    dynamicBody.ApplyForce(forceProvider.GetForceByDynamicBody(dynamicBody) * Time.fixedDeltaTime);
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