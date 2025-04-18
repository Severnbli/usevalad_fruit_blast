using _Project.Scripts.Features.Physics.Objects;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions
{
    public class CollisionResolver: IShapeVisitor
    {
        public void Visit(PhysicsCircle c1, PhysicsCircle c2)
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
                return; // Objects don't collide
            }
            
            var normal = c2Point - c1Point;
            ApplyImpulse(c1, c2, normal.normalized, c1Radius + c2Radius - normal.magnitude);
        }

        public void Visit(PhysicsRectangle r1, PhysicsRectangle r2)
        {
            var r1PointA = r1.PointA;
            var r1PointB = r1.PointB;
            var r2PointA = r2.PointA;
            var r2PointB = r2.PointB;

            if (r1PointA.y > r2PointB.y || r2PointA.y > r1PointB.y
                                        || r1PointA.x > r2PointB.x || r2PointA.x > r1PointB.x)
            {
                return; // Objects don't collide
            }
            
            var dx = Mathf.Min(r1PointB.x, r2PointB.x) - Mathf.Max(r1PointA.x, r2PointA.x);
            var dy = Mathf.Min(r1PointB.y, r2PointB.y) - Mathf.Max(r1PointA.y, r2PointA.y);

            if (dx < dy)
            {
                var normal = r1.GetCenter().x < r2.GetCenter().x ? Vector2.left : Vector2.right;
                ApplyImpulse(r1, r2, normal, dx);
            }
            else
            {
                var normal = r1.GetCenter().y < r2.GetCenter().y ? Vector2.down : Vector2.up;
                ApplyImpulse(r1, r2, normal, dy);
            }
        }

        public void Visit(PhysicsCircle c, PhysicsRectangle r)
        {
            Visit(r, c);
        }
        
        public void Visit(PhysicsRectangle r, PhysicsCircle c)
        {
            var rPointA = r.PointA;
            var rPointB = r.PointB;
            var cPoint = c.Point;
            var cRadius = c.Radius;

            if (cPoint.x + cRadius < rPointA.x || rPointB.x < cPoint.x - cRadius
                                               || cPoint.y + cRadius < rPointA.y || rPointB.y < cPoint.y - cRadius)
            {
                return; // Objects don't collide
            }

            Vector2 p;
            p.x = Mathf.Clamp(cPoint.x, rPointA.x, rPointB.x);
            p.y = Mathf.Clamp(cPoint.y, rPointA.y, rPointB.y);
            
            var d = cPoint - p;

            ApplyImpulse(r, c, d.normalized, cRadius - p.magnitude);
        }

        private void ApplyImpulse(PhysicsObject obj1, PhysicsObject obj2, Vector2 normal, float depth)
        {
            var v12 = obj1.Velocity - obj2.Velocity;
            var vAlongNormal = Vector2.Dot(v12, normal);
            
            var e = Mathf.Min(obj1.BouncinessFactor, obj2.BouncinessFactor);
            
            var j = - (1f + e) * vAlongNormal / (1f / obj1.Mass + 1f / obj2.Mass);
            var impulse = j * normal;
            
            // Debug.Log($"Obj1 {obj1.name}, obj2 {obj2.name}");
            // Debug.Log($"v12: {v12}, normal: {normal}, vAlongNormal: {vAlongNormal}, e: {e}, j: {j}, impulse: {impulse}");
            // Debug.Log($"Initial velocity obj1 {obj1.Velocity}, obj2 {obj2.Velocity}");
            
            obj1.Velocity -= impulse / obj1.Mass * depth;
            obj2.Velocity += impulse / obj2.Mass * depth;
            
            // Debug.Log($"Modified velocity obj1 {obj1.Velocity}, obj2 {obj2.Velocity}");
            
            // PositionalCorrection(obj1, obj2, normal, depth);
        }

        private void PositionalCorrection(PhysicsObject obj1, PhysicsObject obj2, Vector2 normal, float depth)
        {
            const float slop = 0.05f;

            if (depth - slop > 0f)
            {
                var correction = normal * depth;

                Debug.Log(
                    $"position of {obj1.name} {obj1.transform.position}, of {obj2.name} {obj2.transform.position}");

                if (!obj1.IsStatic)
                {
                    obj1.transform.position += new Vector3(correction.x, correction.y) * obj2.Mass;
                }

                if (!obj2.IsStatic)
                {
                    obj2.transform.position -= new Vector3(correction.x, correction.y) * obj1.Mass;
                }
                
                Debug.Log(
                    $"position of {obj1.name} {obj1.transform.position}, of {obj2.name} {obj2.transform.position}");
            }
        }
    }
}