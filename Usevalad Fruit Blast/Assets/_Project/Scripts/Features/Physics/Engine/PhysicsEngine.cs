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
        [SerializeField] private float _minBodySpeed = 0.1f;
        [SerializeField] private float _maxBodySpeed = 100f;
        [SerializeField] private int _collisionResolvingIterations = 6;
        
        private CollisionResolver _collisionResolver;
        
        public readonly List<BaseCollider> Colliders = new();
        public readonly List<DynamicBody> DynamicBodies = new();
        public readonly List<ForceProvider> ForceProviders = new();
        
        public void FixedUpdate()
        {
            MoveEntities();
            ResolveCollisions();
            ApplyForces();
        }

        private void ResolveCollisions()
        {
            for (var k = 0; k < _collisionResolvingIterations; k++)
            {
                for (var i = 0; i < Colliders.Count - 1; i++)
                {
                    for (var j = i + 1; j < Colliders.Count; j++)
                    {
                        Colliders[i].ResolveCollision(Colliders[j], _collisionResolver);
                    }
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
                if (dynamicBody.IsStatic || dynamicBody.Velocity.magnitude < _minBodySpeed)
                {
                    dynamicBody.Velocity = Vector3.zero;
                    continue;
                }

                if (dynamicBody.Velocity.magnitude > _maxBodySpeed)
                {
                    Destroy(dynamicBody.gameObject);
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
            
            _minBodySpeed = physicsEngineConfig.MinBodySpeed;
            _maxBodySpeed = physicsEngineConfig.MaxBodySpeed;
            _collisionResolvingIterations = physicsEngineConfig.CollisionResolvingIterations;
        }
    }
}