using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Controls.Pointer;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider;
using _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner;
using _Project.Scripts.Features.Physics.Services.Gyroscope.GyroscopeGravityChanger;
using _Project.Scripts.Features.Stats.Health;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine
{
    public class LifecycleContainer
    {
        public SystemCoordinator SystemCoordinator { get; private set; }
        public FieldCatcher FieldCatcher { get; private set; }
        public PointerProvider[] PointerProviders { get; private set; }
        public GyroscopeGravityChanger GyroscopeGravityChanger { get; private set; }
        public FieldCatcherSpawner FieldCatcherSpawner { get; private set; }
        public HealthProvider HealthProvider { get; private set; }

        public LifecycleContainer(LifecycleStateMachine lifecycleStateMachine)
        {
            if (ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                SystemCoordinator = systemCoordinator;
            }
            
            var context = lifecycleStateMachine.Context;

            if (context.TryGetComponentFromContainer(out FieldCatcher fieldCatcher))
            {
                FieldCatcher = fieldCatcher;
            }
            
            if (context.TryGetComponentsFromContainer(out PointerProvider[] pointerProviders))
            {
                PointerProviders = pointerProviders;
            }

            if (context.TryGetComponentFromContainer(out GyroscopeGravityChanger gyroscopeGravityChanger))
            {
                GyroscopeGravityChanger = gyroscopeGravityChanger;
            }

            if (context.TryGetComponentFromContainer(out FieldCatcherSpawner fieldCatcherSpawner))
            {
                FieldCatcherSpawner = fieldCatcherSpawner;
            }

            if (context.TryGetComponentFromContainer(out HealthProvider healthProvider))
            {
                HealthProvider = healthProvider;
            }
        }

        public void SetPointerProvidersEnableStatus(bool isEnable)
        {
            foreach (var pointerProvider in PointerProviders)
            {
                pointerProvider?.SetIsEnable(isEnable);
            }
        }
    }
}