using _Project.Scripts.Features.Physics.Services.Collisions;
using _Project.Scripts.Repositories;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Objects
{
    public abstract class PhysicsObject: MonoBehaviour
    {
        [Header("Physics")]
        [Space(5)]
        [SerializeField] private float _mass;
        [SerializeField] private float _gravityFactor;
        [SerializeField] private float _bouncinessFactor;
        [SerializeField] private bool _isStatic;
        [SerializeField] private bool _useGravity = true;

        private CollisionResolver _collisionResolver = new();

        public Vector2 Velocity { get; set; }
        
        public float Mass { get => _mass; set => _mass = value; }
        public float GravityFactor { get => _gravityFactor; set => _gravityFactor = value; }
        public float BouncinessFactor { get => _bouncinessFactor; set => _bouncinessFactor = value; }
        public bool IsStatic { get => _isStatic; set => _isStatic = value; }
        public bool UseGravity { get => _useGravity; set => _useGravity = value; }
        
        public virtual void Start()
        {
            var engine = Context.Container.GetComponent<PhysicsEngine>();

            if (engine == null)
            {
                Debug.LogError($"Physics engine not found");
                return;
            }
            
            engine.Entities.Add(this);
        }
        
        public abstract void Accept(IShapeVisitor visitor, PhysicsObject other);
        public abstract void AcceptCircle(IShapeVisitor visitor, PhysicsCircle c);
        public abstract void AcceptRectangle(IShapeVisitor visitor, PhysicsRectangle r);
        
        public abstract float GetArea();

        public void ResolveCollision(PhysicsObject other)
        {
            var visitor = new CollisionResolver();
            Accept(visitor, other);
        }

        public abstract Vector2 GetCenter();
    }
}