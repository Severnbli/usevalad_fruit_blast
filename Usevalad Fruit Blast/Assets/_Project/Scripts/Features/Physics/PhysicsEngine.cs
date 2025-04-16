using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Features.Physics
{
    public class PhysicsEngine: MonoBehaviour
    {
        public static readonly float BaseGravity = 9.8f; 
        public List<PhysicsObject> Entities { get; } = new();

        public void Update()
        {
            ApplyForces();
            // ResolveCollisions();
            MoveEntities();
        }

        private void ApplyForces()
        {
            foreach (var entity in Entities)
            {
                if (entity.IsStatic)
                {
                    continue;
                }
                
                var totalForces = Vector3.zero;
                
                totalForces.y -= entity.GravityFactor * BaseGravity;

                entity.Velocity += totalForces;
            }
        }

        private void ResolveCollisions()
        {
            // TODO: physics objects collisions
            throw new NotImplementedException();
        }

        private void MoveEntities()
        {
            foreach (var entity in Entities)
            {
                entity.transform.Translate(entity.Velocity * Time.deltaTime);
            }
        }
    }
}