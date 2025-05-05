using UnityEngine;

namespace _Project.Scripts.Features.Physics.Figures
{
    public interface IPhysicsFigure
    {
        public float GetArea();
        public Vector2 GetCenter();
        public void GetBoundingRectangle(out Vector2 min, out Vector2 max);
    }
}