using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver.Config;
using _Project.Scripts.Features.Physics.Services.Visitors;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver
{
    public class CollisionResolver : IColliderVisitorWithOther
    {
        private float _positionCorrectionPercent = 0.25f;
        private float _positionCorrectionSlop = 0.1f;
        
        public CollisionResolver() {}

        public CollisionResolver(CollisionResolverConfig config)
        {
            _positionCorrectionPercent = config.PositionCorrectionPercent;
            _positionCorrectionSlop = config.PositionCorrectionSlop;
        }
        
        public void Visit(CircleCollider c1, CircleCollider c2)
        {
            if (!IsCirclesCollide(c1, c2))
            {
                return;
            }
            
            var normal = c1.Point - c2.Point;
            var displacement = c1.Radius + c2.Radius - normal.magnitude;
            
            ResolveCollision(c1, c2, normal.normalized, displacement);
        }

        public void Visit(RectangleCollider r1, RectangleCollider r2)
        {
            if (!IsRectanglesCollide(r1, r2))
            {
                return;
            }

            CalculateMinSeparationNormalAndDepth(r1, r2, out var normal, out var depth);

            ResolveCollision(r1, r2, normal, depth);
        }

        public void Visit(CircleCollider c, RectangleCollider r)
        {
            if (!IsCircleRectangleCollider(c, r))
            {
                return;
            }
            
            CalculateMinSeparationNormalAndDepth(c, r, out var normal, out var depth);

            ResolveCollision(c, r, normal, depth);
        }
        
        public void Visit(RectangleCollider r, CircleCollider c)
        {
            if (!IsCircleRectangleCollider(c, r))
            {
                return;
            }
            
            CalculateMinSeparationNormalAndDepth(r, c, out var normal, out var depth);

            ResolveCollision(r, c, -normal, depth);
        }

        private void ResolveCollision(BaseCollider obj1, BaseCollider obj2, Vector2 normal, float depth)
        {
            if (ReferenceEquals(obj1.DynamicBody, null) || ReferenceEquals(obj2.DynamicBody, null)
                || !obj1.IsCollide || !obj2.IsCollide)
            {
                return;
            }
            
            if (ApplyImpulse(obj1.DynamicBody, obj2.DynamicBody, normal))
            {
                PositionalCorrection(obj1.DynamicBody, obj2.DynamicBody, normal, depth);
            }
        }

        private bool ApplyImpulse(DynamicBody obj1, DynamicBody obj2, Vector2 normal)
        {
            if (obj1.IsStatic && obj2.IsStatic)
            {
                return false;
            }
            
            var relativeVelocity = obj1.Velocity - obj2.Velocity;
            var velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);
            
            var massObj1 = obj1.IsStatic ? Mathf.Infinity : obj1.Mass;
            var massObj2 = obj2.IsStatic ? Mathf.Infinity : obj2.Mass;
            
            var e = Mathf.Max(obj1.BouncinessFactor, obj2.BouncinessFactor);
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

        private void PositionalCorrection(DynamicBody obj1, DynamicBody obj2, Vector2 normal, float depth)
        {
            var correctionDepth = Mathf.Max(depth - _positionCorrectionSlop, 0f);
            var correction = correctionDepth * _positionCorrectionPercent * normal;

            Vector3 correctionObj1;
            Vector3 correctionObj2;

            var staticState = (obj1.IsStatic, obj2.IsStatic);

            switch (staticState)
            {
                case (true, false):
                {
                    correction *= -1;

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
                    correction *= (obj1.Mass + obj2.Mass);
                
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
        
        private void CalculateMinSeparationNormalAndDepth(BaseCollider obj1, BaseCollider obj2, 
            out Vector2 normal, out float depth)
        {
            obj1.GetBoundingRectangle(out var minPos1, out var maxPos1);
            obj2.GetBoundingRectangle(out var minPos2, out var maxPos2);
            
            var distanceToLowerX = Mathf.Abs(maxPos1.x - minPos2.x);
            var distanceToUpperX = Mathf.Abs(maxPos2.x - minPos1.x);
            var distanceToLowerY = Mathf.Abs(maxPos1.y - minPos2.y);
            var distanceToUpperY = Mathf.Abs(maxPos2.y - minPos1.y);
            
            depth = Mathf.Min(
                Mathf.Min(distanceToLowerX, distanceToUpperX),
                Mathf.Min(distanceToLowerY, distanceToUpperY)
            );
            
            if (distanceToLowerX.Equals(depth))
            {
                normal = Vector2.left;
            }
            else if (distanceToUpperX.Equals(depth))
            {
                normal = Vector2.right;
            }
            else if (distanceToLowerY.Equals(depth))
            {
                normal = Vector2.down;
            }
            else
            {
                normal = Vector2.up;
            }
        }

        public static bool IsCirclesCollide(CircleCollider c1, CircleCollider c2)
        {
            var c1Point = c1.Point;
            var c1Radius = c1.Radius;
            var c2Point = c2.Point;
            var c2Radius = c2.Radius;
            
            double dx = c1Point.x - c2Point.x;
            double dy = c1Point.y - c2Point.y;
            double d = c1Radius + c2Radius;

            return !(dx * dx + dy * dy > d * d);
        }

        public static bool IsRectanglesCollide(RectangleCollider r1, RectangleCollider r2)
        {
            var r1PointA = r1.PointA;
            var r1PointB = r1.PointB;
            var r2PointA = r2.PointA;
            var r2PointB = r2.PointB;

            return !(r1PointA.y > r2PointB.y
                   || r2PointA.y > r1PointB.y
                   || r1PointA.x > r2PointB.x
                   || r2PointA.x > r1PointB.x);
        }

        public static bool IsCircleRectangleCollider(CircleCollider c, RectangleCollider r)
        {
            var rPointA = r.PointA;
            var rPointB = r.PointB;
            var cPoint = c.Point;
            var cRadius = c.Radius;

            return !(cPoint.x + cRadius < rPointA.x
                     || rPointB.x < cPoint.x - cRadius
                     || cPoint.y + cRadius < rPointA.y
                     || rPointB.y < cPoint.y - cRadius);
        }
    }
}