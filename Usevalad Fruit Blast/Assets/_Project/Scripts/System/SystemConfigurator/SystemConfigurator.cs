using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider;
using _Project.Scripts.Features.Physics.Colliders.ColliderDrawer;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Forces;
using _Project.Scripts.Features.Physics.Forces.GravityForceProvider;
using UnityEngine;

namespace _Project.Scripts.System.SystemConfigurator
{
    public class SystemConfigurator : MonoBehaviour
    {
        [SerializeField] private SystemConfig _systemConfig;
        
        public SystemConfig SystemConfig => _systemConfig;
        
        private void OnEnable()
        {
            Setup();
        }

        private void OnDisable()
        {
            Context.ClearContext();
        }
        
        private void Setup()
        {
            SetupContext();
            SetupFeatures();
        }

        private void SetupContext()
        {
            Context.SetupContext(out var container, out var otherScopeFeature);
            container.transform.parent = transform;
        }

        private void SetupFeatures()
        {
            Context.Container.AddFeature<ColliderDrawer>();
            Context.Container.AddFeatureWithConfig<PhysicsEngine, PhysicsEngineConfig>(
                _systemConfig.PhysicsEngineConfig);
            Context.Container.AddFeatureWithConfig<GravityForceProvider, ForceProviderConfig>(
                _systemConfig.GravityForceProviderConfig);
            Context.Container.AddFeatureWithConfig<CameraFieldProvider, CameraFieldProviderConfig>(
                _systemConfig.CameraFieldProviderConfig);
            Context.Container.AddFeatureWithConfig<ColliderFieldCatcher, FieldCatcherConfig>(
                _systemConfig.FieldCatcherConfig);
        }
    }
}