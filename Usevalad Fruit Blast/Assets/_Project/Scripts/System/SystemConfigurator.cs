using _Project.Scripts.Common.Configs;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Gizmo.GizmoDrawer.PhysicsGizmoDrawer;
using _Project.Scripts.Features.Gizmo.GizmoProvider.BaseGizmoProvider;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Forces.GravityForce;
using _Project.Scripts.Features.Random;
using UnityEngine;

namespace _Project.Scripts.System
{
    public class SystemConfigurator : MonoBehaviour
    {
        [SerializeField] private CommonConfig _commonConfig;

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
            
            Context.Container.AddFeature<PhysicsEngine>(_commonConfig.PhysicsEngineConfig);
            Context.Container.AddFeature<BaseGizmoProvider>(_commonConfig.BaseGizmoProviderConfig);
            Context.Container.AddFeature<PhysicsGizmoDrawer>(null);
            Context.Container.AddFeature<GravityForceProvider>(_commonConfig.GravityForceProviderConfig);
            Context.Container.AddFeature<RandomProvider>(_commonConfig.RandomProviderConfig);
            
            _commonConfig.BallSpawner.Init(null);
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