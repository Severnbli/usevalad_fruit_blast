using _Project.Scripts.Features.Physics.Figures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public class RectangleCollider : BaseCollider
    {
        [SerializeField, HideLabel] private RectangleFigure _rectangleFigure = new();

        public RectangleFigure RectangleFigure { get => _rectangleFigure; set => _rectangleFigure = value; }

        public override IPhysicsFigure GetUnmodifiedFigure()
        {
            return _rectangleFigure;
        }

        public override IPhysicsFigure GetFigure() => GetModifiedRectangleFigure();

        public RectangleFigure GetModifiedRectangleFigure()
        {
            var rectangleFigure = _rectangleFigure.Clone();

            rectangleFigure.PointAA *= GetMaxScale();
            rectangleFigure.PointAA += GetPosition();
                    
            rectangleFigure.PointBB *= GetMaxScale();
            rectangleFigure.PointBB += GetPosition();
                
            return rectangleFigure;
        }
    }
}