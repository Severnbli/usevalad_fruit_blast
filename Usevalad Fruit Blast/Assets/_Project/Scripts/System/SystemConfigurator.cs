using _Project.Scripts.Common.Configs;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider;
using _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider;
using _Project.Scripts.Features.Gizmo.GizmoDrawer.PhysicsGizmoDrawer;
using _Project.Scripts.Features.Gizmo.GizmoProvider.BaseGizmoProvider;
using _Project.Scripts.Features.Physics;
using _Project.Scripts.Features.Physics.Forces.GravityForce;
using UnityEngine;

namespace _Project.Scripts.System
{
    public class SystemConfigurator : MonoBehaviour
    {
        [SerializeField] private CommonConfig _commonConfig;
        [SerializeField] private FieldCatcher _fieldCatcher;
        [SerializeField] private FieldProvider _fieldProvider;

        private void OnEnable()
        {
            Setup();
        }

        private void Setup()
        {
            SetupContext();
            SetupComponents();
        }

        private void SetupContext()
        {
            var container = Context.SetupContainer();
            container.transform.parent = transform;
        }

        private void SetupComponents()
        {
            if (Context.Container == null)
            {
                Debug.LogError("Check setups: container is not found!");
            }
            
            Context.Container.AddFeature<PhysicsEngine>(null);
            Context.Container.AddFeature<BaseGizmoProvider>(_commonConfig.BaseGizmoProviderConfig);
            Context.Container.AddFeature<PhysicsGizmoDrawer>(null);
            Context.Container.AddFeature<GravityForceProvider>(_commonConfig.GravityForceProviderConfig);
        }

        private void OnDisable()
        {
            ClearContext();
        }

        private void ClearContext()
        {
            Context.ClearContainer();
        }
    }
}