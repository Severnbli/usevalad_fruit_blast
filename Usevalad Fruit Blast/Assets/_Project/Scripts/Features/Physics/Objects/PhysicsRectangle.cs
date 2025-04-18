using _Project.Scripts.Features.Physics.Services.Collisions;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace _Project.Scripts.Features.Physics.Objects
{
    public class PhysicsRectangle: PhysicsObject
    {
        [Header("Rectangle settings")]
        [Space(5)]
        [SerializeField] private Vector2 _pointA;
        [SerializeField] private Vector2 _pointB;

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

        public void OnDrawGizmosSelected()
        {
            var baseColor = Gizmos.color;
            Gizmos.color = Color.cyan;
            
            var pointA = PointA;
            var pointB = PointB;
            
            Gizmos.DrawLine(new Vector3(pointA.x, pointB.y), pointB);
            Gizmos.DrawLine(new Vector3(pointA.x, pointB.y), pointA);
            Gizmos.DrawLine(new Vector3(pointB.x, pointA.y), pointB);
            Gizmos.DrawLine(new Vector3(pointB.x, pointA.y), pointA);
            
            Gizmos.color = baseColor;
        }
        
        public override void Accept(IShapeVisitor visitor, PhysicsObject other)
        {
            other.AcceptRectangle(visitor, this);
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
            var massCenter = PointB;
            massCenter.x -= (_pointB.x - _pointA.x) / 2;
            massCenter.y -= (_pointB.y - _pointA.y) / 2;
            
            return massCenter;
        }
    }
}