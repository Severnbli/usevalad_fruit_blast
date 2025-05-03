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
            var point = circle.Point;
            
            Gizmos.DrawWireSphere(new Vector3(point.x, point.y), circle.Radius);
        }

        private void DrawRectangleGizmo(RectangleCollider rectangle)
        {
            var pointA = rectangle.PointA;
            var pointB = rectangle.PointB;
            
            Gizmos.DrawLine(new Vector3(pointA.x, pointB.y), pointB);
            Gizmos.DrawLine(new Vector3(pointA.x, pointB.y), pointA);
            Gizmos.DrawLine(new Vector3(pointB.x, pointA.y), pointB);
            Gizmos.DrawLine(new Vector3(pointB.x, pointA.y), pointA);
        }
    }
}