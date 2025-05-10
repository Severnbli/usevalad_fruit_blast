using _Project.Scripts.Features.Physics.Figures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public sealed class CircleCollider : BaseCollider
    {
        [SerializeField, HideLabel] private CircleFigure _circleFigure;

        public CircleFigure CircleFigure {
            get
            {
                var circleFigure = _circleFigure;

                circleFigure.Point += GetPosition();
                circleFigure.Radius *= GetMaxScale();
            
                return circleFigure;
            }
            set => _circleFigure = value;
        }

        public CircleFigure UnModifiedCircleFigure => _circleFigure;

        public override float GetArea()
        {
            return CircleFigure.GetArea();
        }

        public override CircleFigure GetBoundingCircleFigure()
        {
            return CircleFigure;
        }

        public override RectangleFigure GetBoundingRectangleFigure()
        {
            return CircleFigure.GetBoundingRectangleFigure();
        }
    }
}