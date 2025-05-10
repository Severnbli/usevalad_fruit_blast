using _Project.Scripts.Features.Physics.Figures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public sealed class RectangleCollider : BaseCollider
    {
        [SerializeField, HideLabel] private RectangleFigure _rectangleFigure;

        public RectangleFigure RectangleFigure {
            get
            {
                var rectangleFigure = _rectangleFigure;

                rectangleFigure.PointAA *= GetMaxScale();
                rectangleFigure.PointAA += GetPosition();
                    
                rectangleFigure.PointBB *= GetMaxScale();
                rectangleFigure.PointBB += GetPosition();
                
                return rectangleFigure;
            }
            set => _rectangleFigure = value; 
        }

        public RectangleFigure UnModifiedRectangleFigure => _rectangleFigure;
        
        public override float GetArea()
        {
            return RectangleFigure.GetArea();
        }

        public override CircleFigure GetBoundingCircleFigure()
        {
            return RectangleFigure.GetBoundingCircleFigure();
        }

        public override RectangleFigure GetBoundingRectangleFigure()
        {
            return RectangleFigure;
        }
    }
}