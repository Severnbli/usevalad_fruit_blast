using _Project.Scripts.Repositories;
using UnityEngine;

namespace _Project.Scripts.Features.Physics
{
    public abstract class PhysicsObject: MonoBehaviour
    {
        [Header("Physics")]
        [Space(5)]
        [SerializeField] private float _mass;
        [SerializeField] private float _gravityFactor;
        [SerializeField] private float _bouncinessFactor;
        [SerializeField] private bool _isStatic;

        public Vector3 Velocity { get; set; } = new();
        
        public float Mass { get => _mass; set => _mass = value; }
        public float GravityFactor { get => _gravityFactor / 1000f; set => _gravityFactor = value; }
        public float BouncinessFactor { get => _bouncinessFactor; set => _bouncinessFactor = value; }
        public bool IsStatic { get => _isStatic; set => _isStatic = value; }
        
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
        
        public abstract float GetArea();
    }
}