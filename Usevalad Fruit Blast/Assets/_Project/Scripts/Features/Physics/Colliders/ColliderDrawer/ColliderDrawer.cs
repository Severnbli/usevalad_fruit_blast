using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders.ColliderDrawer
{
    public class ColliderDrawer : BaseFeature
    {
        [SerializeField] private Color _gizmoColor = Color.cyan;
         
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            var colliders = GetComponents<BaseCollider>();

            foreach (var collider in colliders)
            {
                DrawCollider(collider);
            }
        }

        private void DrawCollider(BaseCollider collider)
        {
            switch (collider)
            {
                case CircleCollider circle:
                {
                    DrawCircleGizmo(circle);
                    break;
                }
                case RectangleCollider rectangle:
                {
                    DrawRectangleGizmo(rectangle);
                    break;
                }
                default:
                {
                    return;
                }
            }
        }

        private void DrawCircleGizmo(CircleCollider circle)
        {
            var circleFigure = circle.GetModifiedCircleFigure();
            
            Gizmos.DrawWireSphere(new Vector3(circleFigure.Point.x, circleFigure.Point.y), circleFigure.Radius);
        }

        private void DrawRectangleGizmo(RectangleCollider rectangle)
        {
            var rectangleFigure = rectangle.GetModifiedRectangleFigure();
            
            Gizmos.DrawLine(new Vector3(rectangleFigure.PointAA.x, rectangleFigure.PointBB.y), rectangleFigure.PointBB);
            Gizmos.DrawLine(new Vector3(rectangleFigure.PointAA.x, rectangleFigure.PointBB.y), rectangleFigure.PointAA);
            Gizmos.DrawLine(new Vector3(rectangleFigure.PointBB.x, rectangleFigure.PointAA.y), rectangleFigure.PointBB);
            Gizmos.DrawLine(new Vector3(rectangleFigure.PointBB.x, rectangleFigure.PointAA.y), rectangleFigure.PointAA);
        }
    }
}