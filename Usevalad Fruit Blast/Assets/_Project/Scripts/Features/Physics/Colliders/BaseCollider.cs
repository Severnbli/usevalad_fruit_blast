using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Figures;
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

            DynamicBody?.Colliders.Remove(this);
        }

        public float GetMaxScale()
        {
            return Mathf.Max(transform.localScale.x, transform.localScale.y);
        }

        public Vector2 GetPosition()
        {
            return new Vector2(transform.position.x, transform.position.y);
        }

        public abstract IPhysicsFigure GetUnmodifiedFigure();
        public abstract IPhysicsFigure GetFigure();
    }
}