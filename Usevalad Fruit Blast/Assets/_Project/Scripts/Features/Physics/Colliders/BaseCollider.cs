using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public abstract class BaseCollider : MonoBehaviour
    {
        [SerializeField] private bool _isTrigger = false;
        
        public bool IsTrigger { get => _isTrigger; set => _isTrigger = value; }
        public DynamicBody DynamicBody { get; set; }
        
        protected void Start()
        {
            if (!Context.TryGetComponentFromContainer(out PhysicsEngine engine))
            {
                Debug.LogError("Physics engine not found");
                return;
            }
            
            engine.Colliders.Add(this);
        }
        
        protected void OnDestroy()
        {
            if (Context.TryGetComponentFromContainer(out PhysicsEngine engine))
            {
                engine.Colliders.Remove(this);
            }
        }
        
        public abstract float GetArea();
        public abstract Vector2 GetCenter();
        public abstract void GetBoundingRectangle(out Vector2 min, out Vector2 max);
    }
}