using System.Collections.Generic;
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
        [SerializeField] private float _bouncinessFactor = 0.5f;
        [SerializeField] private bool _isStatic;
        [SerializeField] private bool _useGravity = true;
        
        public bool IsSleep { get; set; }
        
        public List<BaseCollider> Colliders { get; private set; } = new();
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
                Colliders.Add(collider);
            }
            
            if (!Context.TryGetComponentFromContainer(out PhysicsEngine engine))
            {
                Debug.LogError("Physics engine not found");
                return;
            }
            
            engine.DynamicBodies.Add(this);
        }
        
        protected void OnDestroy()
        {
            if (!Context.TryGetComponentFromContainer(out PhysicsEngine engine))
            {
                return;
            }
            
            engine.DynamicBodies.Remove(this);
        }
        
        public void ApplyForce(Vector2 force)
        {
            Velocity += force;
        }
    }
}