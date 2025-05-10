using System.Collections.Generic;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Forces;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver;
using Unity.Profiling;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Engine
{
    public class PhysicsEngine : BaseFeature, IConfigurableFeature<PhysicsEngineConfig>, IFixedUpdatableFeature
    {
        [SerializeField] private PhysicsEngineConfig _physicsEngineConfig;
        
        private CollisionResolver _collisionResolver;
        
        public PhysicsEngineConfig PhysicsEngineConfig => _physicsEngineConfig;
        public readonly List<BaseCollider> Colliders = new();
        public readonly List<DynamicBody> DynamicBodies = new();
        public readonly List<ForceProvider> ForceProviders = new();
        
        public void Configure(PhysicsEngineConfig physicsEngineConfig)
        {
            _physicsEngineConfig = physicsEngineConfig;
            _collisionResolver = new CollisionResolver(_physicsEngineConfig.CollisionResolverConfig);
        }
        
        public void FixedUpdate()
        {
            MoveEntities();
            ResolveCollisions();
            ApplyForces();
        }

        private void ResolveCollisions()
        {
            _collisionResolver.IterativeResolveCollisions(Colliders);
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
                if (dynamicBody.IsStatic)
                {
                    dynamicBody.Velocity = Vector3.zero;
                    continue;
                }

                if (dynamicBody.Velocity.magnitude < _physicsEngineConfig.MinBodySpeed)
                {
                    dynamicBody.IsSleep = true;
                    dynamicBody.Velocity = Vector3.zero;
                    continue;
                }
                
                dynamicBody.IsSleep = false;
        
                if (dynamicBody.Velocity.magnitude > _physicsEngineConfig.MaxBodySpeed)
                {
                    Object.Destroy(dynamicBody.gameObject);
                }
        
                dynamicBody.transform.Translate(dynamicBody.Velocity * Time.fixedDeltaTime);
            }
        }
    }
}