using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public class RectangleCollider : BaseCollider
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
            var scaleFactor = Mathf.Max(transform.localScale.x, transform.localScale.y);
            
            return Mathf.Abs((_pointA.x - _pointB.x) * scaleFactor * (_pointA.y - _pointB.y) * scaleFactor);
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