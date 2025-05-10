using System;
using _Project.Scripts.Common.Objects;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Figures
{
    [Serializable]
    public struct PointFigure : IPhysicsFigure, ICloneableObject<PointFigure>
    {
        [SerializeField] private Vector2 _point;
        
        public Vector2 Point { get => _point; set => _point = value; }
        
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

        public RectangleFigure GetBoundingRectangleFigure()
        {
            return new RectangleFigure(_point, _point);
        }

        public CircleFigure GetBoundingCircleFigure()
        {
            return new CircleFigure(_point, 0f);
        }
    }
}