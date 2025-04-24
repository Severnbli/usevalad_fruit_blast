using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver;
using _Project.Scripts.Features.Physics.Services.Visitors;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public abstract class BaseCollider: MonoBehaviour
    {
        [SerializeField] private bool _isTrigger = false;
        
        public bool IsTrigger { get => _isTrigger; set => _isTrigger = value; }
        public DynamicBody DynamicBody { get; set; }
        
        protected void Start()
        {
            if (!Context.Container.TryGetComponent(out PhysicsEngine engine))
            {
                Debug.LogError($"Physics engine not found");
                return;
            }
            
            engine.Colliders.Add(this);
        }
        
        protected void OnDestroy()
        {
            if (Context.Container != null && Context.Container.TryGetComponent(out PhysicsEngine engine))
            {
                engine.Colliders.Remove(this);
            }
        }
        
        public abstract void Accept(IColliderVisitor visitor);
        public abstract void Accept(IColliderVisitorWithOther visitorWithOther, BaseCollider other);
        public abstract void AcceptCircle(IColliderVisitorWithOther visitorWithOther, CircleCollider c);
        public abstract void AcceptRectangle(IColliderVisitorWithOther visitorWithOther, RectangleCollider r);
        
        public abstract float GetArea();
        public abstract Vector2 GetCenter();
        public abstract void GetBoundingRectangle(out Vector2 min, out Vector2 max);

        public void ResolveCollision(BaseCollider other, CollisionResolver collisionResolver)
        {
            Accept(collisionResolver, other);
        }

        public void SetIsTrigger(bool isTrigger)
        {
            _isTrigger = isTrigger;
        }
    }
}