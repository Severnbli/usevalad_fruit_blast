using _Project.Scripts.Features.Gizmo.GizmoProvider;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Services.Visitors;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Gizmo
{
    public class ColliderGizmoDrawer : IColliderVisitor
    {
        public GizmoProvider GizmoProvider { get; set; }

        public ColliderGizmoDrawer(GizmoProvider gizmoProvider)
        {
            GizmoProvider = gizmoProvider;
        }
        
        public void Visit(CircleCollider c)
        {
            var point = c.Point;
            
            Gizmos.color = GizmoProvider.GetGizmoColor();
            
            Gizmos.DrawWireSphere(new Vector3(point.x, point.y), c.Radius);
        }

        public void Visit(RectangleCollider c)
        {
            var pointA = c.PointA;
            var pointB = c.PointB;
            
            Gizmos.color = GizmoProvider.GetGizmoColor();
            
            Gizmos.DrawLine(new Vector3(pointA.x, pointB.y), pointB);
            Gizmos.DrawLine(new Vector3(pointA.x, pointB.y), pointA);
            Gizmos.DrawLine(new Vector3(pointB.x, pointA.y), pointB);
            Gizmos.DrawLine(new Vector3(pointB.x, pointA.y), pointA);
        }
    }
}