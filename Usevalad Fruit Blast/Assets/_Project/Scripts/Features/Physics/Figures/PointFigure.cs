using System;
using _Project.Scripts.Common.Objects;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Figures
{
    [Serializable]
    public class PointFigure : IPhysicsFigure, ICloneableObject<PointFigure>
    {
        [SerializeField] private Vector2 _point;
        
        public Vector2 Point { get => _point; set => _point = value; }
        
        public PointFigure() {}
        
        public PointFigure(Vector2 point)
        {
            _point = point;
        }

        public PointFigure Clone() => new(_point);

        public float GetArea()
        {
            return 0f;
        }

        public Vector2 GetCenter()
        {
            return _point;
        }

        public void GetBoundingRectangle(out Vector2 min, out Vector2 max)
        {
            min = _point;
            max = _point;
        }
    }
}