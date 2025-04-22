using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Services.Visitors;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions
{
    public class CollisionResolver : IColliderVisitorWithOther
    {
        public void Visit(CircleCollider c1, CircleCollider c2)
        {
            var c1Point = c1.Point;
            var c1Radius = c1.Radius;
            var c2Point = c2.Point;
            var c2Radius = c2.Radius;
            
            double dx = c1Point.x - c2Point.x;
            double dy = c1Point.y - c2Point.y;
            double d = c1Radius + c2Radius;

            if (dx * dx + dy * dy > d * d)
            {
                return;
            }
            
            var normal = c2Point - c1Point;
            ResolveCollision(c1, c2, normal.normalized, c1Radius + c2Radius - normal.magnitude);
        }

        public void Visit(RectangleCollider r1, RectangleCollider r2)
        {
            var r1PointA = r1.PointA;
            var r1PointB = r1.PointB;
            var r2PointA = r2.PointA;
            var r2PointB = r2.PointB;

            if (r1PointA.y > r2PointB.y || r2PointA.y > r1PointB.y
                                        || r1PointA.x > r2PointB.x || r2PointA.x > r1PointB.x)
            {
                return;
            }
            
            var dx = Mathf.Min(r1PointB.x, r2PointB.x) - Mathf.Max(r1PointA.x, r2PointA.x);
            var dy = Mathf.Min(r1PointB.y, r2PointB.y) - Mathf.Max(r1PointA.y, r2PointA.y);

            Vector2 normal;
            float displacement;
            
            if (dx < dy)
            {
                normal = r1.GetCenter().x < r2.GetCenter().x ? Vector2.left : Vector2.right;
                displacement = dx;
            }
            else
            {
                normal = r1.GetCenter().y < r2.GetCenter().y ? Vector2.down : Vector2.up;
                displacement = dy;
            }
            
            ResolveCollision(r1, r2, normal, displacement);
        }

        public void Visit(CircleCollider c, RectangleCollider r)
        {
            Visit(r, c);
        }
        
        public void Visit(RectangleCollider r, CircleCollider c)
        {
            var rPointA = r.PointA;
            var rPointB = r.PointB;
            var cPoint = c.Point;
            var cRadius = c.Radius;

            if (cPoint.x + cRadius < rPointA.x || rPointB.x < cPoint.x - cRadius
                                               || cPoint.y + cRadius < rPointA.y || rPointB.y < cPoint.y - cRadius)
            {
                return;
            }

            var p = new Vector2(
                Mathf.Clamp(cPoint.x, rPointA.x, rPointB.x),
                Mathf.Clamp(cPoint.y, rPointA.y, rPointB.y)
            );
            
            Vector2 d = cPoint - p;
            float dist = d.magnitude;

            if (dist >= cRadius)
                return;

            Vector2 normal;
            if (dist > 0f)
            {
                normal = d / dist;
            }
            else
            {
                var left = cPoint.x - rPointA.x;
                var right = rPointB.x - cPoint.x;
                var up = rPointB.y - cPoint.y;
                var down = cPoint.y - rPointA.y;

                var min = Mathf.Min(left, right, up, down);
                
                if (min.Equals(left))
                {
                    normal = Vector2.left;
                }
                else if (min.Equals(right))
                {
                    normal = Vector2.right;
                }
                else if (min.Equals(up))
                {
                    normal = Vector2.up;
                }
                else
                {
                    normal = Vector2.down;
                }
            }

            var displacement = cRadius - dist;

            ResolveCollision(r, c, normal, displacement);
        }

        private void ResolveCollision(BaseCollider obj1, BaseCollider obj2, Vector2 normal, float depth)
        {
            if (ReferenceEquals(obj1.DynamicBody, null) || ReferenceEquals(obj2.DynamicBody, null))
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

            // if (velocityAlongNormal > 0)
            // {
            //     return false;
            // }
            
            var e = Mathf.Min(obj1.BouncinessFactor, obj2.BouncinessFactor);
            var j = -(1f + e) * velocityAlongNormal / (1f / obj1.Mass + 1f / obj2.Mass);
            
            var impulse = j * normal;
            
            // Debug.Log($"normal: {normal}");
            // Debug.Log($"relativeVelocity: {relativeVelocity}, velocityAlongNormal: {velocityAlongNormal}, j: {j}");
            // Debug.Log($"Impulse: {impulse}, obj1.Velocity: {obj1.Velocity}, obj2.Velocity: {obj2.Velocity}");

            if (!obj1.IsStatic)
            {
                obj1.Velocity += impulse / obj1.Mass;
            }

            if (!obj2.IsStatic)
            {
                obj2.Velocity -= impulse / obj2.Mass;
            }
            
            // Debug.Log($"obj1.Velocity: {obj1.Velocity}, obj2.Velocity: {obj2.Velocity}");

            return true;
        }

        private void PositionalCorrection(DynamicBody obj1, DynamicBody obj2, Vector2 normal, float depth)
        {
            const float percent = 0.2f;
            const float slop = 0.05f;

            var correctionDepth = Mathf.Max(depth - slop, 0f);
            var correction = correctionDepth / (obj1.Mass + obj2.Mass) * percent * normal;

            if (!obj1.IsStatic)
            {
                obj1.transform.position += new Vector3(correction.x, correction.y) * obj2.Mass;
            }

            if (!obj2.IsStatic)
            {
                obj2.transform.position -= new Vector3(correction.x, correction.y) * obj1.Mass;
            }
        }
    }
}