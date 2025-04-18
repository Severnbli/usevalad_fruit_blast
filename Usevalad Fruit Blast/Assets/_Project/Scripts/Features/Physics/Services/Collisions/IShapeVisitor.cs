using _Project.Scripts.Features.Physics.Objects;

namespace _Project.Scripts.Features.Physics.Services.Collisions
{
    public interface IShapeVisitor
    {
        void Visit(PhysicsCircle c1, PhysicsCircle c2);
        void Visit(PhysicsRectangle r1, PhysicsRectangle r2);
        void Visit(PhysicsCircle c, PhysicsRectangle r);
        void Visit(PhysicsRectangle r, PhysicsCircle c);
    }
}