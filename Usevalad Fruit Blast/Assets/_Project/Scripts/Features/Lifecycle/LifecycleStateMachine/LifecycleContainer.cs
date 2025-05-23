using System;
using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Controls.Pointer;
using _Project.Scripts.Features.Effects.Objects.EffectObjectsContainer;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner;
using _Project.Scripts.Features.Physics.Services.Gyroscope.GyroscopeGravityChanger;
using _Project.Scripts.Features.Stats.Experience;
using _Project.Scripts.Features.Stats.Health;
using _Project.Scripts.Features.UI.Screens.PauseScreen;
using _Project.Scripts.Features.UI.UIProvider;

namespace _Project.Scripts.Features.Lifecycle.LifecycleStateMachine
{
    public class LifecycleContainer
    {
        private SystemCoordinator _systemCoordinator;
        private FieldCatcher _fieldCatcher;
        private PointerProvider[] _pointerProviders;
        private GyroscopeGravityChanger _gyroscopeGravityChanger;
        private FieldCatcherSpawner _fieldCatcherSpawner;
        private HealthProvider _healthProvider;
        private ObjectsContainer _objectsContainer;
        private EffectObjectsContainer _effectObjectsContainer;
        private UIProvider _uiProvider;
        private ExperienceProvider _experienceProvider;
        
        public SystemCoordinator SystemCoordinator => _systemCoordinator;
        public FieldCatcher FieldCatcher => _fieldCatcher;
        public PointerProvider[] PointerProviders => _pointerProviders;
        public GyroscopeGravityChanger GyroscopeGravityChanger => _gyroscopeGravityChanger;
        public FieldCatcherSpawner FieldCatcherSpawner => _fieldCatcherSpawner;
        public HealthProvider HealthProvider => _healthProvider;
        public ObjectsContainer ObjectsContainer => _objectsContainer;
        public EffectObjectsContainer EffectObjectsContainer => _effectObjectsContainer;
        public UIProvider UIProvider => _uiProvider;
        public ExperienceProvider ExperienceProvider => _experienceProvider;

        public event Action<bool> OnChangeUserInputAvailability;
        public bool UserInputAvailability { get; private set; }

        public LifecycleContainer(LifecycleStateMachine lifecycleStateMachine)
        {
            if (!ObjectFinder.TryFindObjectByType(out _systemCoordinator))
            {
                return;
            }
            
            var context = _systemCoordinator.Context;

            context.TryGetComponentFromContainer(out _fieldCatcher);
            context.TryGetComponentsFromContainer(out _pointerProviders);
            context.TryGetComponentFromContainer(out _gyroscopeGravityChanger);
            context.TryGetComponentFromContainer(out _fieldCatcherSpawner);
            context.TryGetComponentFromContainer(out _healthProvider);
            context.TryGetComponentFromContainer(out _objectsContainer);
            context.TryGetComponentFromContainer(out _effectObjectsContainer);
            context.TryGetComponentFromContainer(out _uiProvider);
            context.TryGetComponentFromContainer(out _experienceProvider);
        }

        public void SetPointerProvidersEnableStatus(bool isEnable)
        {
            foreach (var pointerProvider in PointerProviders)
            {
                pointerProvider?.SetIsEnable(isEnable);
            }
        }

        public void ChangeUserInputAvailability(bool isAvailable)
        {
            OnChangeUserInputAvailability?.Invoke(isAvailable);
            UserInputAvailability = isAvailable;
        }
    }
}