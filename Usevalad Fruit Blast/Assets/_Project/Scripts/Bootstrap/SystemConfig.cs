using System;
using _Project.Scripts.Features.Dimensions.Scale.ScaleProvider;
using _Project.Scripts.Features.Effects.Objects.EffectObjectsContainer;
using _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider;
using _Project.Scripts.Features.Effects.Providers.ExplosionEffectProvider;
using _Project.Scripts.Features.Effects.Providers.SplitSpriteEffectProvider;
using _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider;
using _Project.Scripts.Features.Lifecycle.Destroyers.ClickObjectDestroyer;
using _Project.Scripts.Features.Lifecycle.GameTime.GameTimeProvider;
using _Project.Scripts.Features.Lifecycle.LifecycleStateMachine;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Forces;
using _Project.Scripts.Features.Physics.Services.Explosions.ExplosionProvider;
using _Project.Scripts.Features.Physics.Services.Gyroscope.GyroscopeGravityChanger;
using _Project.Scripts.Features.Random;
using _Project.Scripts.Features.Stats.Experience;
using _Project.Scripts.Features.Stats.Health;
using _Project.Scripts.Features.Stats.Health.HealthInfluencers;
using _Project.Scripts.Features.UI.UIProvider;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Bootstrap
{
    [Serializable]
    public class SystemConfig
    {
        [SerializeField] private GameTimeProviderConfig _gameTimeProviderConfig;
        [SerializeField] private UIProviderConfig _uiProviderConfig;
        [SerializeField] private PhysicsEngineConfig _physicsEngineConfig;
        [SerializeField] private ForceProviderConfig _gravityForceProviderConfig;
        [SerializeField] private ForceProviderConfig _explosionForceProviderConfig;
        [SerializeField, InlineProperty] private CameraFieldProviderConfig _cameraFieldProviderConfig;
        [SerializeField] private ColliderFieldCatcherConfig _colliderFieldCatcherConfig;
        [SerializeField] private RandomProviderConfig _randomProviderConfig;
        [SerializeField, InlineProperty] private ObjectsContainerConfig _objectsContainerConfig;
        [SerializeField] private ExplosionProviderConfig _explosionProviderConfig;
        [SerializeField] private FieldCatcherSpawnerConfig _fieldCatcherSpawnerConfig;
        [SerializeField] private ScaleProviderConfig _scaleProviderConfig;
        [SerializeField] private ClickObjectDestroyerConfig _clickObjectDestroyerConfig;
        [SerializeField] private GyroscopeGravityChangerConfig _gyroscopeGravityChangerConfig;
        [SerializeField, InlineProperty] private EffectObjectsContainerConfig _effectObjectsContainerConfig;
        [SerializeField] private SplitSpriteEffectProviderConfig _splitSpriteEffectProviderConfig;
        [SerializeField, InlineProperty] private HealthProviderConfig _healthProviderConfig;
        [SerializeField, InlineProperty] private ExperienceProviderConfig _experienceProviderConfig;
        [SerializeField] private HealthInfluencerConfig _healthClickObjectDestroyerInfluencerConfig;
        [SerializeField] private ExperienceEffectProviderConfig _experienceEffectProviderConfig;
        [SerializeField] private ExplosionEffectProviderConfig _explosionEffectProviderConfig;
        [SerializeField] private LifecycleStateMachineConfig _lifecycleStateMachineConfig;
        
        public GameTimeProviderConfig GameTimeProviderConfig => _gameTimeProviderConfig;
        public UIProviderConfig UIProviderConfig => _uiProviderConfig;
        public PhysicsEngineConfig PhysicsEngineConfig => _physicsEngineConfig;
        public ForceProviderConfig GravityForceProviderConfig => _gravityForceProviderConfig;
        public ForceProviderConfig ExplosionForceProviderConfig => _explosionForceProviderConfig;
        public CameraFieldProviderConfig CameraFieldProviderConfig => _cameraFieldProviderConfig;
        public ColliderFieldCatcherConfig ColliderFieldCatcherConfig => _colliderFieldCatcherConfig;
        public RandomProviderConfig RandomProviderConfig => _randomProviderConfig;
        public ObjectsContainerConfig ObjectsContainerConfig => _objectsContainerConfig;
        public ExplosionProviderConfig ExplosionProviderConfig => _explosionProviderConfig;
        public FieldCatcherSpawnerConfig FieldCatcherSpawnerConfig => _fieldCatcherSpawnerConfig;
        public ScaleProviderConfig ScaleProviderConfig => _scaleProviderConfig;
        public ClickObjectDestroyerConfig ClickObjectDestroyerConfig => _clickObjectDestroyerConfig;
        public GyroscopeGravityChangerConfig GyroscopeGravityChangerConfig => _gyroscopeGravityChangerConfig;
        public EffectObjectsContainerConfig EffectObjectsContainerConfig => _effectObjectsContainerConfig;
        public SplitSpriteEffectProviderConfig SplitSpriteEffectProviderConfig => _splitSpriteEffectProviderConfig;
        public HealthProviderConfig HealthProviderConfig => _healthProviderConfig;
        public ExperienceProviderConfig ExperienceProviderConfig => _experienceProviderConfig;
        public HealthInfluencerConfig HealthClickObjectDestroyerInfluencerConfig => _healthClickObjectDestroyerInfluencerConfig;
        public ExperienceEffectProviderConfig ExperienceEffectProviderConfig => _experienceEffectProviderConfig;
        public ExplosionEffectProviderConfig ExplosionEffectProviderConfig => _explosionEffectProviderConfig;
        public LifecycleStateMachineConfig LifecycleStateMachineConfig => _lifecycleStateMachineConfig;
    }
}