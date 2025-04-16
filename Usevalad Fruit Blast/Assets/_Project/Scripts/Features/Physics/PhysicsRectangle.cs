using System;
using UnityEngine;

namespace _Project.Scripts.Features.Physics
{
    public class PhysicsRectangle: PhysicsObject
    {
        [Header("Rectangle settings")]
        [Space(5)]
        [SerializeField] private Vector2 _point1;
        [SerializeField] private Vector2 _point2;

        public Vector2 Point1
        {
            get => new(transform.position.x + transform.localScale.x * _point1.x, 
                transform.position.y + transform.localScale.y * _point1.y); 
            set => _point1 = value;
        }

        public Vector2 Point2
        {
            get => new(transform.position.x + transform.localScale.x * _point2.x, 
                transform.position.y + transform.localScale.y *_point2.y);
            set => _point2 = value;
        }
        
        public override float GetArea()
        {
            return Mathf.Abs((_point1.x - _point2.x) * (_point1.y - _point2.y));
        }

        public void OnDrawGizmosSelected()
        {
            var baseColor = Gizmos.color;
            Gizmos.color = Color.cyan;
            
            var point1 = Point1;
            var point2 = Point2;
            
            Gizmos.DrawLine(new Vector3(point1.x, point2.y), point2);
            Gizmos.DrawLine(new Vector3(point1.x, point2.y), point1);
            Gizmos.DrawLine(new Vector3(point2.x, point1.y), point2);
            Gizmos.DrawLine(new Vector3(point2.x, point1.y), point1);
            
            Gizmos.color = baseColor;
        }
    }
}