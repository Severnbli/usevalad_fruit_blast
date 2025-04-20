using System.Collections.Generic;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Forces;
using _Project.Scripts.Features.Physics.Kinematics;
using _Project.Scripts.Features.Physics.Services.Collisions;
using UnityEngine;

namespace _Project.Scripts.Features.Physics
{
    public class PhysicsEngine: BaseFeature
    { 
        private readonly CollisionResolver _collisionResolver = new();
        
        public List<BaseCollider> Colliders { get; } = new();
        public List<KinematicBody> KinematicBodies { get; } = new();
        public List<ForceProvider> ForceProviders { get; } = new();
        
        public void FixedUpdate()
        {
            ApplyForces();
            ResolveCollisions();
            MoveEntities();
        }

        private void ApplyForces()
        {
            foreach (var kinematicBody in KinematicBodies)
            {
                foreach (var forceProvider in ForceProviders)
                {
                    kinematicBody.ApplyForce(forceProvider.GetForce() * Time.deltaTime);
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
            foreach (var kinematicBody in KinematicBodies)
            {
                if (kinematicBody.IsStatic)
                {
                    kinematicBody.Velocity = Vector3.zero;
                    continue;
                }
                
                kinematicBody.transform.Translate(kinematicBody.Velocity * Time.deltaTime);
            }
        }

        public override void Init(IFeatureConfig config) {}
    }
}