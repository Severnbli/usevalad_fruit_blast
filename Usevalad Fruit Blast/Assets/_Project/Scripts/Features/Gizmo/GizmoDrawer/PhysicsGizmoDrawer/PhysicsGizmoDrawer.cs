using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Services.Gizmo;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Gizmo.GizmoDrawer.PhysicsGizmoDrawer
{
    public class PhysicsGizmoDrawer : GizmoDrawer
    {
        [SerializeField] private PhysicsEngine _physicsEngine;

        private ColliderGizmoDrawer _colliderGizmoDrawer;
        
        public PhysicsEngine PhysicsEngine { get => _physicsEngine; set => _physicsEngine = value; }

        public override void Init(IFeatureConfig config)
        {
            base.Init(config);

            _colliderGizmoDrawer = new ColliderGizmoDrawer(_gizmoProvider);
            
            _physicsEngine = Context.Container.GetComponent<PhysicsEngine>();

            if (_physicsEngine == null)
            {
                Debug.LogError("Check system priority setup: physics engine must be earlier than physics gizmo drawer!");
            }
        }

        private void OnDrawGizmos()
        {
            var colliders = _physicsEngine.GetColliders();
            
            foreach (var collider in colliders)
            {
                collider.Accept(_colliderGizmoDrawer);
            }
        }
    }
}