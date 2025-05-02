using _Project.Scripts.Features.Physics.Services.Visitors;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public class RectangleCollider: BaseCollider
    {
        [SerializeField] private Vector2 _pointA = Vector2.zero;
        [SerializeField] private Vector2 _pointB = Vector2.zero;

        /// <summary>
        /// PointA is a bottom left point of the rectangle
        /// </summary>
        public Vector2 PointA
        {
            get => new(transform.position.x + transform.localScale.x * _pointA.x, 
                transform.position.y + transform.localScale.y * _pointA.y); 
            set => _pointA = value;
        }

        /// <summary>
        /// PointB is a top right point of the rectangle
        /// </summary>
        public Vector2 PointB
        {
            get => new(transform.position.x + transform.localScale.x * _pointB.x, 
                transform.position.y + transform.localScale.y *_pointB.y);
            set => _pointB = value;
        }
        
        public override float GetArea()
        {
            return Mathf.Abs((_pointA.x - _pointB.x) * (_pointA.y - _pointB.y));
        }
        
        public override void Accept(IColliderVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public override void Accept(IColliderVisitorWithOther visitorWithOther, BaseCollider other)
        {
            other.AcceptRectangle(visitorWithOther, this);
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
            var massCenter = PointB;
            massCenter.x -= (_pointB.x - _pointA.x) / 2;
            massCenter.y -= (_pointB.y - _pointA.y) / 2;
            
            return massCenter;
        }

        public override void GetBoundingRectangle(out Vector2 min, out Vector2 max)
        {
            min = PointA;
            max = PointB;
        }
    }
}