using _Project.Scripts.Features.Physics.Services.Visitors;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public class CircleCollider: BaseCollider
    {
        [SerializeField] private Vector2 _point;

        [SerializeField] private float _radius;

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

        public override void Accept(IColliderVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Accept(IColliderVisitorWithOther visitorWithOther, BaseCollider other)
        {
            other.AcceptCircle(visitorWithOther, this);
        }

        public override void AcceptCircle(IColliderVisitorWithOther visitorWithOther, CircleCollider c)
        {
            visitorWithOther.Visit(c, this);
        }

        public override void AcceptRectangle(IColliderVisitorWithOther visitorWithOther, RectangleCollider r)
        {
            visitorWithOther.Visit(r, this);
        }

        public override Vector2 GetCenter()
        {
            return Point;
        }

        public override void GetBoundingRectangle(out Vector2 min, out Vector2 max)
        {
            var point = Point;
            var radius = Radius;
            
            min = new Vector2(point.x - radius, point.y - radius);
            max = new Vector2(point.x + radius, point.y + radius);
        }

        public override bool Equals(object other)
        {
            return other is CircleCollider otherCircle && Equals(otherCircle);
        }

        public bool Equals(CircleCollider other)
        {
            return Point.Equals(other.Point) && Radius.Equals(other.Radius);
        }

        public override int GetHashCode()
        {
            return Point.GetHashCode() ^ Radius.GetHashCode();
        }
    }
}