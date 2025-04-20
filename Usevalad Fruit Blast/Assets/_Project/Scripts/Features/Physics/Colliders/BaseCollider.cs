using _Project.Scripts.Features.Physics.Kinematics;
using _Project.Scripts.Features.Physics.Services.Collisions;
using _Project.Scripts.Features.Physics.Services.Visitors;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public abstract class BaseCollider: MonoBehaviour
    {
        [SerializeField] private bool _isCollide;
        
        public bool IsCollide { get => _isCollide; }
        public KinematicBody KinematicBody { get; set; }
        
        public virtual void Start()
        {
            if (!Context.Container.TryGetComponent(out PhysicsEngine engine))
            {
                Debug.LogError($"Physics engine not found");
                return;
            }
            
            engine.Colliders.Add(this);
        }
        
        public abstract void Accept(IColliderVisitor visitor);
        public abstract void Accept(IColliderVisitorWithOther visitorWithOther, BaseCollider other);
        public abstract void AcceptCircle(IColliderVisitorWithOther visitorWithOther, CircleCollider c);
        public abstract void AcceptRectangle(IColliderVisitorWithOther visitorWithOther, RectangleCollider r);
        
        public abstract float GetArea();
        public abstract Vector2 GetCenter();

        public void ResolveCollision(BaseCollider other, CollisionResolver collisionResolver)
        {
            Accept(collisionResolver, other);
        }

        public void SetIsCollide(bool isCollide)
        {
            _isCollide = isCollide;
        }

        protected void OnDestroy()
        {
            if (Context.Container != null && Context.Container.TryGetComponent(out PhysicsEngine engine))
            {
                engine.Colliders.Remove(this);
            }
        }

        public override bool Equals(object other)
        {
            return other is BaseCollider;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}