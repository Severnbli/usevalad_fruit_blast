using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Controls.Pointer;
using _Project.Scripts.Features.Physics.Services.Gyroscope.GyroscopeGravityChanger;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine
{
    public class LifecycleContainer
    {
        public SystemCoordinator SystemCoordinator { get; private set; }
        public PointerProvider[] PointerProviders { get; private set; }
        public GyroscopeGravityChanger GyroscopeGravityChanger { get; private set; }

        public LifecycleContainer(LifecycleStateMachine lifecycleStateMachine)
        {
            var context = lifecycleStateMachine.Context;

            if (context.TryGetComponentsFromContainer(out PointerProvider[] pointerProviders))
            {
                PointerProviders = pointerProviders;
            }

            if (context.TryGetComponentFromContainer(out GyroscopeGravityChanger gyroscopeGravityChanger))
            {
                GyroscopeGravityChanger = gyroscopeGravityChanger;
            }

            if (ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                SystemCoordinator = systemCoordinator;
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