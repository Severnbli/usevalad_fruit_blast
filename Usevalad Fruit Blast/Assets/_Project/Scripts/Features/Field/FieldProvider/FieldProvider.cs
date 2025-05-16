using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.Physics.Figures;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionFinder;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldProvider
{
    public abstract class FieldProvider : BaseFeature
    {
        public abstract Vector2 GetFieldSize();
        public abstract Vector2 GetFieldPosition();
        public abstract Vector2 GetConvertedScreenSpacePosition(Vector2 screenSpacePosition);

        public virtual RectangleFigure GetFieldRectangle()
        {
            var fieldPosition = GetFieldPosition();
            var fieldSize = GetFieldSize();
            
            return new RectangleFigure(
                new Vector2(fieldPosition.x - fieldSize.x / 2f, fieldPosition.y - fieldSize.y / 2f),
                new Vector2(fieldPosition.x + fieldSize.x / 2f, fieldPosition.y + fieldSize.y / 2f));
        }

        public virtual bool IsObjectOutOfScreen(GameObject gameObject, float offset = 0)
        {
            var rectangleFigure = GetFieldRectangle();
            var offsetVector = new Vector2(offset, offset);
            
            rectangleFigure.PointAA -= offsetVector;
            rectangleFigure.PointBB += offsetVector;
            
            var pointFigure = new PointFigure(gameObject.transform.position);
            
            return CollisionFinder.IsPointRectangleCollide(pointFigure, rectangleFigure);
        }
    }
}