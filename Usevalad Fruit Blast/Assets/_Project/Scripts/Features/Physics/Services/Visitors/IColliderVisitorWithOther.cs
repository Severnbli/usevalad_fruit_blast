using _Project.Scripts.Features.Physics.Colliders;

namespace _Project.Scripts.Features.Physics.Services.Visitors
{
    public interface IColliderVisitorWithOther
    {
        void Visit(CircleCollider c1, CircleCollider c2);
        void Visit(RectangleCollider r1, RectangleCollider r2);
        void Visit(CircleCollider c, RectangleCollider r);
        void Visit(RectangleCollider r, CircleCollider c);
    }
}