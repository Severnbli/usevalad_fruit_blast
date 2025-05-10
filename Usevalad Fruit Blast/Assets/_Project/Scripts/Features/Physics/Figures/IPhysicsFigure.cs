using UnityEngine;

namespace _Project.Scripts.Features.Physics.Figures
{
    public interface IPhysicsFigure
    {
        public float GetArea();
        public Vector2 GetCenter();
        public RectangleFigure GetBoundingRectangleFigure();
        public CircleFigure GetBoundingCircleFigure();
    }
}