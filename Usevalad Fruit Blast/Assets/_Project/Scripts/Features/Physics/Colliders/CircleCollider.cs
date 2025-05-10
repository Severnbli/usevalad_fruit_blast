using _Project.Scripts.Features.Physics.Figures;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public sealed class CircleCollider : BaseCollider
    {
        [SerializeField, HideLabel] private CircleFigure _circleFigure = new();

        public CircleFigure CircleFigure { get => _circleFigure; set => _circleFigure = value; }

        public override IPhysicsFigure GetUnmodifiedFigure()
        {
            return _circleFigure;
        }

        public override IPhysicsFigure GetFigure() => GetModifiedCircleFigure();

        public CircleFigure GetModifiedCircleFigure()
        {
            var circleFigure = _circleFigure.Clone();

            circleFigure.Point += GetPosition();
            circleFigure.Radius *= GetMaxScale();
            
            return circleFigure;
        }
    }
}