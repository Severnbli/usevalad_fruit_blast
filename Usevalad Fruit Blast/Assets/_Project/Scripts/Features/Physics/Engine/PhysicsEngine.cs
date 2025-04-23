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
        
        private readonly HashSet<BaseCollider> _colliders = new();
        private readonly HashSet<DynamicBody> _dynamicBodies = new();
        private readonly HashSet<ForceProvider> _forceProviders = new();
        
        public void FixedUpdate()
        {
            var dynamicBodies = GetDynamicBodies();
            var forceProviders = GetForceProviders();
            var colliders = GetColliders();
            
            MoveEntities(dynamicBodies);
            ResolveCollisions(colliders);
            ApplyForces(dynamicBodies, forceProviders);
        }

        private void ApplyForces(List<DynamicBody> dynamicBodies, List<ForceProvider> forceProviders)
        {
            foreach (var dynamicBody in dynamicBodies)
            {
                
                foreach (var forceProvider in forceProviders)
                {
                    dynamicBody.ApplyForce(forceProvider.GetForce() * Time.fixedDeltaTime);
                }
            }
        }

        private void ResolveCollisions(List<BaseCollider> colliders)
        {
            for (var i = 0; i < colliders.Count - 1; i++)
            {
                
                for (var j = i + 1; j < colliders.Count; j++)
                {
                    colliders[i].ResolveCollision(colliders[j], _collisionResolver);
                }
            }
        }

        private void MoveEntities(List<DynamicBody> dynamicBodies)
        {
            foreach (var dynamicBody in dynamicBodies)
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
        
        public List<BaseCollider> GetColliders()
        {
            var colliders = _colliders.ToList();

            for (var i = colliders.Count - 1; i >= 0; i--)
            {
                if (colliders[i] == null)
                {
                    colliders.RemoveAt(i);
                }
            }
            
            return colliders;
        }
        
        public List<DynamicBody> GetDynamicBodies()
        {
            var dynamicBodies = _dynamicBodies.ToList();

            for (var i = dynamicBodies.Count - 1; i >= 0; i--)
            {
                if (dynamicBodies[i] == null)
                {
                    dynamicBodies.RemoveAt(i);
                }
            }
            
            return dynamicBodies;
        }
        
        public List<ForceProvider> GetForceProviders()
        {
            var forceProviders = _forceProviders.ToList();

            for (var i = forceProviders.Count - 1; i >= 0; i--)
            {
                if (forceProviders[i] == null)
                {
                    forceProviders.RemoveAt(i);
                }
            }
            
            return forceProviders;
        }

        public void AddCollider(BaseCollider collider)
        {
            _colliders.Add(collider);
        }

        public void RemoveCollider(BaseCollider collider)
        {
            _colliders.Remove(collider);
        }

        public void AddDynamicBody(DynamicBody dynamicBody)
        {
            _dynamicBodies.Add(dynamicBody);
        }

        public void RemoveDynamicBody(DynamicBody dynamicBody)
        {
            _dynamicBodies.Remove(dynamicBody);
        }

        public void AddForceProvider(ForceProvider forceProvider)
        {
            _forceProviders.Add(forceProvider);
        }

        public void RemoveForceProvider(ForceProvider forceProvider)
        {
            _forceProviders.Remove(forceProvider);
        }
    }
}