using System.Collections.Generic;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Services.Collisions.Triggers;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver
{
    public class CollisionResolver
    {
        private readonly CollisionResolverConfig _collisionResolverConfig;
        private readonly CollisionFinder.CollisionFinder _collisionFinder;

        public CollisionResolver(CollisionResolverConfig collisionResolverConfig)
        {
            _collisionResolverConfig = collisionResolverConfig;
            _collisionFinder = new CollisionFinder.CollisionFinder();
        }

        public void IterativeResolveCollisions(List<BaseCollider> colliders)
        {
            ResolveCollisionsWithImpulse(colliders);
            
            for (var i = 0; i < _collisionResolverConfig.CollisionResolvingIterations - 1; i++)
            {
                ResolveCollisions(colliders);
            }
        }

        public void ResolveCollisions(List<BaseCollider> colliders)
        {
            for (var i = 0; i < colliders.Count - 1; i++)
            {
                for (var j = i + 1; j < colliders.Count; j++)
                {
                    if (!IsCollidersSuitable(colliders[i], colliders[j]))
                    {
                        continue;
                    }
                    
                    ResolveCollision(colliders[i], colliders[j]);
                }
            }
        }
        
        public void ResolveCollisionsWithImpulse(List<BaseCollider> colliders)
        {
            for (var i = 0; i < colliders.Count - 1; i++)
            {
                for (var j = i + 1; j < colliders.Count; j++)
                {
                    if (!IsCollidersSuitable(colliders[i], colliders[j]))
                    {
                        continue;
                    }
                    
                    ResolveCollisionWithImpulse(colliders[i], colliders[j]);
                }
            }
        }
        
        public void ResolveCollision(BaseCollider bc1, BaseCollider bc2)
        {
            if (!_collisionFinder.TryFindCollision((bc1, bc2), out var normal, out var depth))
            {
                return;
            }
                
            PositionalCorrection(bc1, bc2, normal, depth);
        }

        public void ResolveCollisionWithImpulse(BaseCollider bc1, BaseCollider bc2)
        {
            if (!_collisionFinder.TryFindCollision((bc1, bc2), out var normal, out var depth))
            {
                return;
            }

            if (TryApplyImpulse(bc1, bc2, normal))
            {
                PositionalCorrection(bc1, bc2, normal, depth);
            }
        }

        public bool IsCollidersSuitable(BaseCollider bc1, BaseCollider bc2)
        {
            if (ReferenceEquals(bc1.DynamicBody, null) || ReferenceEquals(bc2.DynamicBody, null))
            {
                return false;
            }

            if (bc1.IsTrigger || bc2.IsTrigger)
            {
                bc1.GetComponent<ColliderTrigger>()?.OnColliderTriggerEnter(bc2);
                bc2.GetComponent<ColliderTrigger>()?.OnColliderTriggerEnter(bc1);
                
                return false;
            }

            if (bc1.DynamicBody.IsSleep && bc2.DynamicBody.IsSleep)
            {
                return false;
            }
            
            return true;
        }

        private bool TryApplyImpulse(BaseCollider bc1, BaseCollider bc2, Vector2 normal)
        {
            var obj1 = bc1.DynamicBody;
            var obj2 = bc2.DynamicBody;
            
            if (obj1.IsStatic && obj2.IsStatic)
            {
                return false;
            }
            
            var relativeVelocity = obj1.Velocity - obj2.Velocity;
            var velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);
            
            var massObj1 = obj1.IsStatic ? Mathf.Infinity : obj1.Mass;
            var massObj2 = obj2.IsStatic ? Mathf.Infinity : obj2.Mass;
            
            var e = Mathf.Min(obj1.BouncinessFactor, obj2.BouncinessFactor);
            var j = -(1f + e) * velocityAlongNormal / (1f / massObj1 + 1f / massObj2);
            
            var impulse = j * normal;
            
            if (!obj1.IsStatic)
            {
                obj1.Velocity += impulse / obj1.Mass;
            }

            if (!obj2.IsStatic)
            {
                obj2.Velocity -= impulse / obj2.Mass;
            }
            
            return true;
        }

        private void PositionalCorrection(BaseCollider bc1, BaseCollider bc2, Vector2 normal, float depth)
        {
            var obj1 = bc1.DynamicBody;
            var obj2 = bc2.DynamicBody;
            
            var correctionDepth = Mathf.Max(
                depth - _collisionResolverConfig.PositionCorrectionSlop, 
                0f);
            
            var correction = correctionDepth 
                             * _collisionResolverConfig.PositionCorrectionPercent 
                             * normal;

            Vector3 correctionObj1;
            Vector3 correctionObj2;

            var staticState = (obj1.IsStatic, obj2.IsStatic);

            switch (staticState)
            {
                case (true, false):
                {
                    correctionObj1 = Vector3.zero;
                    correctionObj2 = new Vector3(correction.x, correction.y);
                    
                    break;
                }

                case (false, true):
                {
                    correctionObj1 = new Vector3(correction.x, correction.y);
                    correctionObj2 = Vector3.zero;
                    
                    break;
                }

                case (false, false):
                {
                    correction /= (obj1.Mass + obj2.Mass);
                
                    correctionObj1 = new Vector3(correction.x, correction.y) * obj2.Mass;
                    correctionObj2 = new Vector3(correction.x, correction.y) * obj1.Mass;
                    
                    break;
                }

                default:
                {
                    correctionObj1 = Vector3.zero;
                    correctionObj2 = Vector3.zero;
                    
                    break;
                }
            }

            obj1.transform.position += correctionObj1;
            obj2.transform.position -= correctionObj2;
        }
    }
}