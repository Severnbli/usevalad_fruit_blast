using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.System;
using UnityEngine;


namespace _Project.Scripts.Features.Physics.Dynamic
{
    [RequireComponent(typeof(BaseCollider))]
    public class DynamicBody: MonoBehaviour
    {
        [SerializeField] private float _mass = 1f;
        [SerializeField] private float _gravityFactor = 1f;
        [SerializeField] private float _bouncinessFactor;
        [SerializeField] private bool _isStatic;
        [SerializeField] private bool _useGravity = true;
        
        public Vector2 Velocity { get; set; }
        
        public float Mass { get => _mass; set => _mass = value; }
        public float GravityFactor { get => _gravityFactor; set => _gravityFactor = value; }
        public float BouncinessFactor { get => _bouncinessFactor; set => _bouncinessFactor = value; }
        public bool IsStatic { get => _isStatic; set => _isStatic = value; }
        public bool UseGravity { get => _useGravity; set => _useGravity = value; }

        protected void Start()
        {
            var colliders = gameObject.GetComponents<BaseCollider>();

            foreach (var collider in colliders)
            {
                collider.DynamicBody = this;
            }
            
            if (!Context.Container.TryGetComponent(out PhysicsEngine engine))
            {
                Debug.LogError("Physics engine not found");
                return;
            }
            
            engine.AddDynamicBody(this);
        }
        
        protected void OnDestroy()
        {
            if (Context.Container != null && Context.Container.TryGetComponent(out PhysicsEngine engine))
            {
                engine.RemoveDynamicBody(this);
            }
        }
        
        public void ApplyForce(Vector2 force)
        {
            Velocity += force;
        }
    }
}