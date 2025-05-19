using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Figures;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.CollisionFinder
{
    public static class CollisionFinder
    {
        public static bool TryFindCollision(BaseCollider bc1, BaseCollider bc2, 
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;
            
            switch (bc1, bc2)
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

        public static bool TryFindCircleCircleCollision(CircleCollider c1, CircleCollider c2, 
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;

            var c1Figure = c1.CircleFigure;
            var c2Figure = c2.CircleFigure;
            
            if (!IsCircleCircleCollide(c1Figure, c2Figure))
            {
                return false;
            }
            
            CalculateCircleCircleNormalAndDepth(c1Figure, c2Figure, out normal, out depth);
            
            return true;
        }

        public static bool TryFindRectangleRectangleCollision(RectangleCollider r1, RectangleCollider r2,
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;
            
            var r1Figure = r1.RectangleFigure;
            var r2Figure = r2.RectangleFigure;

            if (!IsRectangleRectangleCollide(r1Figure, r2Figure))
            {
                return false;
            }
            
            CalculateMinSeparationNormalAndDepth(r1Figure, r2Figure, out normal, out depth);
            
            return true;
        }
        
        public static bool TryFindCircleRectangleCollision(CircleCollider c, RectangleCollider r,
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;
            
            var cFigure = c.CircleFigure;
            var rFigure = r.RectangleFigure;
            
            if (!IsCircleRectangleCollide(cFigure, rFigure))
            {
                return false;
            }
            
            CalculateMinSeparationNormalAndDepth(cFigure.GetBoundingRectangleFigure(), rFigure, out normal, out depth);
            
            return true;
        }
        
        public static bool TryFindRectangleCircleCollision(RectangleCollider r, CircleCollider c,
            out Vector2 normal, out float depth)
        {
            normal = default;
            depth = 0f;

            var rFigure = r.RectangleFigure;
            var cFigure = c.CircleFigure;
            
            if (!IsCircleRectangleCollide(cFigure, rFigure))
            {
                return false;
            }
            
            CalculateMinSeparationNormalAndDepth(rFigure, cFigure.GetBoundingRectangleFigure(), out normal, out depth);
            
            return true;
        }

        public static bool IsFiguresCollide(IPhysicsFigure figure1, IPhysicsFigure figure2)
        {
            switch (figure1, figure2)
            {
                case (CircleFigure c1, CircleFigure c2):
                {
                    return IsCircleCircleCollide(c1, c2);
                }
                case (RectangleFigure r1, RectangleFigure r2):
                {
                    return IsRectangleRectangleCollide(r1, r2);
                }
                case (CircleFigure c, RectangleFigure r):
                {
                    return IsCircleRectangleCollide(c, r);
                }
                case (RectangleFigure r, CircleFigure c):
                {
                    return IsCircleRectangleCollide(c, r);
                }
                default:
                {
                    return false;
                }
            }
        }

        public static bool IsPointPointCollide(PointFigure p1, PointFigure p2)
        {
            return p1.Point == p2.Point;
        }

        public static bool IsPointCircleCollide(PointFigure p, CircleFigure c)
        {
            var distance = Vector2.Distance(p.Point, c.Point);
            
            return distance < c.Radius;
        }

        public static bool IsPointRectangleCollide(PointFigure p, RectangleFigure r)
        {
            return !(p.Point.x < r.PointAA.x
                     || r.PointBB.x < p.Point.x
                     || p.Point.y < r.PointAA.y
                     || r.PointBB.y < p.Point.y);
        }

        public static bool IsCircleCircleCollide(CircleFigure c1, CircleFigure c2)
        {
            double dx = c1.Point.x - c2.Point.x;
            double dy = c1.Point.y - c2.Point.y;
            double d = c1.Radius + c2.Radius;

            return !(dx * dx + dy * dy > d * d);
        }
        
        public static  bool IsRectangleRectangleCollide(RectangleFigure r1, RectangleFigure r2)
        {
            return !(r1.PointAA.y > r2.PointBB.y
                     || r1.PointAA.y > r2.PointBB.y
                     || r1.PointAA.x > r2.PointBB.x
                     || r1.PointAA.x > r2.PointBB.x);
        }
        
        public static  bool IsCircleRectangleCollide(CircleFigure c, RectangleFigure r)
        {
            return !(c.Point.x + c.Radius < r.PointAA.x
                     || r.PointBB.x < c.Point.x - c.Radius
                     || c.Point.y + c.Radius < r.PointAA.y
                     || r.PointBB.y < c.Point.y - c.Radius);
        }

        private static void CalculateCircleCircleNormalAndDepth(CircleFigure c1, CircleFigure c2,
            out Vector2 normal, out float depth)
        {
            normal = c1.Point - c2.Point;
            depth = c1.Radius + c2.Radius - normal.magnitude;
            normal.Normalize();
        }

        private static void CalculateMinSeparationNormalAndDepth(RectangleFigure f1, RectangleFigure f2, 
            out Vector2 normal, out float depth)
        {
            var distanceToLowerX = Mathf.Abs(f1.PointBB.x - f2.PointAA.x);
            var distanceToUpperX = Mathf.Abs(f2.PointBB.x - f1.PointAA.x);
            var distanceToLowerY = Mathf.Abs(f1.PointBB.y - f2.PointAA.y);
            var distanceToUpperY = Mathf.Abs(f2.PointBB.y - f1.PointAA.y);
            
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