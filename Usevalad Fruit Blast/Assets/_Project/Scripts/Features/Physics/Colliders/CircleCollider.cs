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
    }
}