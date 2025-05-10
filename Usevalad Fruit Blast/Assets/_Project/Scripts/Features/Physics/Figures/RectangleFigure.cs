using System;
using _Project.Scripts.Common.Objects;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Figures
{
    [Serializable]
    public struct RectangleFigure : IPhysicsFigure, ICloneableObject<RectangleFigure>
    {
        [SerializeField] private Vector2 _pointAA;
        [SerializeField] private Vector2 _pointBB;
        
        public Vector2 PointAA { get => _pointAA; set => _pointAA = value; }
        public Vector2 PointBB { get => _pointBB; set => _pointBB = value; }

        public RectangleFigure(Vector2 pointAA, Vector2 pointBB)
        {
            _pointAA = pointAA;
            _pointBB = pointBB;
        }

        public RectangleFigure Clone() => new(_pointAA, _pointBB);
        
        public float GetArea()
        {
            return Mathf.Abs((_pointAA.x - _pointBB.x) * (_pointAA.y - _pointBB.y));
        }
        
        public Vector2 GetCenter()
        {
            var massCenter = _pointBB;
            massCenter.x -= (_pointBB.x - _pointAA.x) / 2f;
            massCenter.y -= (_pointBB.y - _pointAA.y) / 2f;
            
            return massCenter;
        }

        public RectangleFigure GetBoundingRectangleFigure()
        {
            return this;
        }

        public CircleFigure GetBoundingCircleFigure()
        {
            var center = GetCenter();
            var radius = (_pointBB - _pointAA).magnitude / 2f;
            
            return new CircleFigure(center, radius);
        }
    }
}