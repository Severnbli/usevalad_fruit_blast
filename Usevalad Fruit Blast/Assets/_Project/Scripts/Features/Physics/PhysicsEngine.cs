using System.Collections.Generic;
using _Project.Scripts.Features.Physics.Objects;
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
            ResolveCollisions();
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
                
                var totalForces = Vector2.zero;

                if (entity.UseGravity)
                {
                    totalForces.y -= entity.GravityFactor * BaseGravity * Time.deltaTime;
                }

                entity.Velocity += totalForces;
            }
        }

        private void ResolveCollisions()
        {
            for (int i = 0; i < Entities.Count - 1; i++)
            {
                for (int j = i + 1; j < Entities.Count; j++)
                {
                    Entities[i].ResolveCollision(Entities[j]);
                }
            }
        }

        private void MoveEntities()
        {
            foreach (var entity in Entities)
            {
                if (entity.IsStatic)
                {
                    entity.Velocity = Vector3.zero;
                    continue;
                }
                
                entity.transform.Translate(entity.Velocity * Time.deltaTime);
            }
        }
    }
}