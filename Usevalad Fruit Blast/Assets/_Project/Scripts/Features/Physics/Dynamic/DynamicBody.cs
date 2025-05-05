using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Engine;
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
        
        private PhysicsEngine _physicsEngine;
        
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

            if (!ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                return;
            }
            
            if (systemCoordinator.Context.TryGetComponentFromContainer(out _physicsEngine))
            {
                _physicsEngine.DynamicBodies.Add(this);
            }
        }
        
        protected void OnDestroy()
        {
            _physicsEngine?.DynamicBodies.Remove(this);
            
            Colliders
                .Where(x => x != null)
                .ToList()
                .ForEach(x => x.DynamicBody = null);
        }
        
        public void ApplyForce(Vector2 force)
        {
            Velocity += force;
        }
    }
}