using _Project.Scripts.Features.Physics.Services.Collisions;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace _Project.Scripts.Features.Physics.Objects
{
    public class PhysicsCircle: PhysicsObject
    {
        [Header("Circle settings")]
        [Space(5)]
        [SerializeField] private Vector2 _point;

        [SerializeField] private float _radius; // Must be better than zero

        public Vector2 Point
        {
            get => new(transform.position.x + _point.x, transform.position.y + _point.y);
            set => _point = value;
        }

        public float Radius
        {
            get => _radius * Mathf.Max(transform.localScale.x, transform.localScale.y);
            set => _radius = value;
        }
        
        public override float GetArea()
        {
            return Mathf.PI * _radius * _radius;
        }

        public void OnDrawGizmosSelected()
        {
            var baseColor = Gizmos.color;
            Gizmos.color = Color.yellow;

            var point = Point;
            Gizmos.DrawWireSphere(new Vector3(point.x, point.y), Radius);
            
            Gizmos.color = baseColor;
        }

        public override void Accept(IShapeVisitor visitor, PhysicsObject other)
        {
            other.AcceptCircle(visitor, this);
        }

        public override void AcceptCircle(IShapeVisitor visitor, PhysicsCircle c)
        {
            visitor.Visit(c, this);
        }

        public override void AcceptRectangle(IShapeVisitor visitor, PhysicsRectangle r)
        {
            visitor.Visit(r, this);
        }

        public override Vector2 GetCenter()
        {
            return Point;
        }
    }
}