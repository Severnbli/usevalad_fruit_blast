using _Project.Scripts.Features.Physics.Colliders;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.CollisionFinder
{
    public class CollisionFinder
    {
        public bool TryFindCollision((BaseCollider bc1, BaseCollider bc2) pair, 
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;
            
            switch (pair)
            {
                case (CircleCollider c1, CircleCollider c2):
                {
                    return TryFindCircleCircleCollision(c1, c2, out normal, out depth);
                }
                case (RectangleCollider r1, RectangleCollider r2):
                {
                    return TryFindRectangleRectangleCollision(r1, r2, out normal, out depth);
                }
                case (RectangleCollider r, CircleCollider c):
                {
                    return TryFindRectangleCircleCollision(r, c, out normal, out depth);
                }
                case (CircleCollider c, RectangleCollider r):
                {
                    return TryFindCircleRectangleCollision(c, r, out normal, out depth);
                }
            }
            
            return false;
        }

        public bool TryFindCircleCircleCollision(CircleCollider c1, CircleCollider c2, 
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;
            
            if (!IsCircleCircleCollide(c1, c2))
            {
                return false;
            }
            
            CalculateCircleCircleNormalAndDepth(c1, c2, out normal, out depth);
            
            return true;
        }

        public bool TryFindRectangleRectangleCollision(RectangleCollider r1, RectangleCollider r2,
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;

            if (!IsRectangleRectangleCollide(r1, r2))
            {
                return false;
            }
            
            CalculateMinSeparationNormalAndDepth(r1, r2, out normal, out depth);
            
            return true;
        }
        
        public bool TryFindCircleRectangleCollision(CircleCollider c, RectangleCollider r,
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;
            
            if (!IsCircleRectangleCollide(c, r))
            {
                return false;
            }
            
            CalculateMinSeparationNormalAndDepth(c, r, out normal, out depth);
            
            return true;
        }
        
        public bool TryFindRectangleCircleCollision(RectangleCollider r, CircleCollider c,
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;
            
            if (!IsCircleRectangleCollide(c, r))
            {
                return false;
            }
            
            CalculateMinSeparationNormalAndDepth(r, c, out normal, out depth);
            
            return true;
        }

        public bool IsCircleCircleCollide(CircleCollider c1, CircleCollider c2)
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
        
        public bool IsRectangleRectangleCollide(RectangleCollider r1, RectangleCollider r2)
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
        
        public bool IsCircleRectangleCollide(CircleCollider c, RectangleCollider r)
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

        private void CalculateCircleCircleNormalAndDepth(CircleCollider c1, CircleCollider c2,
            out Vector2 normal, out float depth)
        {
            normal = c1.Point - c2.Point;
            normal.Normalize();
            depth = c1.Radius + c2.Radius - normal.magnitude;
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
    }
}