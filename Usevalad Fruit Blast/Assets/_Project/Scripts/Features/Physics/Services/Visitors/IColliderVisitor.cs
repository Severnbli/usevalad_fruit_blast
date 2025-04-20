using _Project.Scripts.Features.Physics.Colliders;

namespace _Project.Scripts.Features.Physics.Services.Visitors
{
    public interface IColliderVisitor
    {
        void Visit(CircleCollider c);
        void Visit(RectangleCollider c);
    }
}