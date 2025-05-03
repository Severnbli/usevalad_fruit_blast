using _Project.Scripts.Features.Physics.Colliders;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.CollisionFinder
{
    public class CollisionFinder
    {
        public static bool TryFindCollision(BaseCollider bc1, BaseCollider bc2, out CollisionPair collisionPair)
        {
            collisionPair = null;
            
            return false;
        }

        public static bool TryFindCircleCircleCollision(CircleCollider c1, CircleCollider c2,
            out CollisionPair collisionPair)
        {
            collisionPair = null;

            if (!IsCircleCircleCollide(c1, c2))
            {
                return false;
            }
            
            CalculateCircleCircleNormalAndDepth(c1, c2, out var normal, out var depth);
            
            collisionPair = new CollisionPair(c1, c2, normal, depth);
            
            return true;
        }

        public static bool TryFindRectangleRectangleCollision(RectangleCollider r1, RectangleCollider r2,
            out CollisionPair collisionPair)
        {
            collisionPair = null;

            if (!IsRectangleRectangleCollide(r1, r2))
            {
                return false;
            }
            
            CalculateRectangleRectangleNormalAndDepth(r1, r2, out var normal, out var depth);
            
            collisionPair = new CollisionPair(r1, r2, normal, depth);
            
            return true;
        }

        public static bool TryFindCircleRectangleCollision(CircleCollider c, RectangleCollider r,
            out CollisionPair collisionPair)
        {
            collisionPair = null;

            if (!IsCircleRectangleCollide(c, r))
            {
                return false;
            }
            
            
        }

        public static bool IsCircleCircleCollide(CircleCollider c1, CircleCollider c2)
        {
            var c1Point = c1.Point;
            var c1Radius = c1.Radius;
            var c2Point = c2.Point;
            var c2Radius = c2.Radius;
            
            double dx = c1Point.x - c2Point.x;
            double dy = c1Point.y - c2Point.y;
            double d = c1Radius + c2Radius;

            return dx * dx + dy * dy <= d * d;
        }
        
        public static bool IsRectangleRectangleCollide(RectangleCollider r1, RectangleCollider r2)
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
        
        public static bool IsCircleRectangleCollide(CircleCollider c, RectangleCollider r)
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

        public static void CalculateCircleCircleNormalAndDepth(CircleCollider c1, CircleCollider c2,
            out Vector2 normal, out float depth)
        {
            normal = c1.Point - c2.Point;
            normal.Normalize();
            depth = c1.Radius + c2.Radius - normal.magnitude;
        }

        public static void CalculateRectangleRectangleNormalAndDepth(RectangleCollider r1, RectangleCollider r2,
            out Vector2 normal, out float depth)
        {
            var r1PointA = r1.PointA;
            var r1PointB = r1.PointB;
            var r2PointA = r2.PointA;
            var r2PointB = r2.PointB;
            
            var distanceToLowerX = Mathf.Abs(r1PointB.x - r2PointA.x);
            var distanceToUpperX = Mathf.Abs(r2PointB.x - r1PointA.x);
            var distanceToLowerY = Mathf.Abs(r1PointB.y - r2PointA.y);
            var distanceToUpperY = Mathf.Abs(r2PointB.y - r1PointA.y);
            
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

        public static void CalculateCircleRectangleNormalAndDepth(CircleCollider c, RectangleCollider r,
            out Vector2 normal, out float depth)
        {
            var cPointA = c.Point;
            var cRadius = c.Radius;
            var rPointA = r.PointA;
            var rPointB = r.PointB;
            
            
        }
    }
}