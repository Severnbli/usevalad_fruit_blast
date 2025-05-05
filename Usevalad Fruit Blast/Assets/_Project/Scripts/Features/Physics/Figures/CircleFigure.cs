using System;
using _Project.Scripts.Common.Objects;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Figures
{
    [Serializable]
    public class CircleFigure : IPhysicsFigure, ICloneableObject<CircleFigure>
    {
        [SerializeField] private Vector2 _point;
        [SerializeField] private float _radius = 0.5f;
        
        public Vector2 Point { get => _point; set => _point = value; }
        public float Radius { get => _radius; set => _radius = value; }
        
        public CircleFigure() {}
        
        public CircleFigure(Vector2 point, float radius)
        {
            _point = point;
            _radius = radius;
        }

        public CircleFigure Clone() => new (_point, _radius);

        public float GetArea()
        {
            return Mathf.PI * _radius * _radius;
        }

        public Vector2 GetCenter()
        {
            return _point;
        }

        public void GetBoundingRectangle(out Vector2 min, out Vector2 max)
        {
            min = new Vector2(_point.x - _radius, _point.y - _radius);
            max = new Vector2(_point.x + _radius, _point.y + _radius);
        }

        public void GetBoundingCircle(out Vector2 center, out float radius)
        {
            center = _point;
            radius = _radius;
        }
    }
}