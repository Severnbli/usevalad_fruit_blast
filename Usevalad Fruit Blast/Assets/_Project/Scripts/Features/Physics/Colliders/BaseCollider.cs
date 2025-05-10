using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Figures;
using _Project.Scripts.Features.Physics.Services.Collisions.Triggers;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Colliders
{
    public abstract class BaseCollider : MonoBehaviour
    {
        private PhysicsEngine _physicsEngine;
        
        [SerializeField] private bool _isTrigger = false;
        
        public bool IsTrigger { get => _isTrigger; set => _isTrigger = value; }
        public ColliderTrigger ColliderTrigger { get; private set; }
        public DynamicBody DynamicBody { get; set; }
        
        protected void Start()
        {
            ColliderTrigger = GetComponent<ColliderTrigger>();
            
            if (!ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                return;
            }

            if (!systemCoordinator.Context.TryGetComponentFromContainer(out _physicsEngine))
            {
                return;
            }
            
            _physicsEngine.Colliders.Add(this);
        }
        
        protected void OnDestroy()
        {
            _physicsEngine?.Colliders.Remove(this);
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