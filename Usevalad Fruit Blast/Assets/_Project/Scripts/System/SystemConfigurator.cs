using _Project.Scripts.Common.Configs;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Controls.Pointer.Mouse.MouseProvider;
using _Project.Scripts.Features.Controls.Pointer.Touch.TouchProvider;
using _Project.Scripts.Features.Destroyers.ClickObjectDestroyer;
using _Project.Scripts.Features.Destroyers.ClickObjectDestroyer.Config;
using _Project.Scripts.Features.Destroyers.Services.InfectiousProvider;
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
            SetupFeatures();
        }

        private void SetupContext()
        {
            var container = Context.SetupContainer();
            container.transform.parent = transform;

            Context.SetupComponents();
        }

        private void SetupFeatures()
        {
            if (Context.Container == null)
            {
                Debug.LogError("Check setups: container is not found!");
            }
            
            Context.Components.Add(_commonConfig.FieldProvider);
            Context.Components.Add(_commonConfig.FieldCatcher);
            Context.Components.Add(_commonConfig.FieldCatcherSpawner);
            
            Context.Container.AddFeature<PhysicsEngine>(_commonConfig.PhysicsEngineConfig);
            Context.Container.AddFeature<BaseGizmoProvider>(_commonConfig.BaseGizmoProviderConfig);
            Context.Container.AddFeature<PhysicsGizmoDrawer>(null);
            Context.Container.AddFeature<GravityForceProvider>(_commonConfig.GravityForceProviderConfig);
            Context.Container.AddFeature<RandomProvider>(_commonConfig.RandomProviderConfig);
            Context.Container.AddFeature<MouseProvider>(null);
            Context.Container.AddFeature<TouchProvider>(null);
            _commonConfig.FieldCatcherSpawner.Init(null);
            Context.Container.AddFeature<InfectiousProvider>(_commonConfig.InfectiousProviderConfig);
            Context.Container.AddFeature<ClickObjectDestroyer>(_commonConfig.ClickObjectDestroyerConfig);
        }

        private void OnDisable()
        {
            ClearContext();
        }

        private void ClearContext()
        {
            Context.ClearContainer();
            Context.ClearComponents();
        }
    }
}