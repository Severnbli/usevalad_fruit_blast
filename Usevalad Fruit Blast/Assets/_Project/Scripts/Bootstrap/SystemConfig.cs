using System;
using System.Runtime.InteropServices;
using _Project.Scripts.Features.Dimensions.Scale.ScaleProvider;
using _Project.Scripts.Features.Effects.Objects.EffectObjectsContainer;
using _Project.Scripts.Features.Effects.Providers.SplitSpriteEffectProvider;
using _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider;
using _Project.Scripts.Features.Lifecycle.Destroyers.ClickObjectDestroyer;
using _Project.Scripts.Features.Lifecycle.LifecycleManager;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Forces;
using _Project.Scripts.Features.Physics.Services.Explosions.ExplosionProvider;
using _Project.Scripts.Features.Physics.Services.Gyroscope.GyroscopeGravityChanger;
using _Project.Scripts.Features.Random;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Bootstrap
{
    [Serializable]
    public class SystemConfig
    {
        [SerializeField] private PhysicsEngineConfig _physicsEngineConfig;
        [SerializeField] private ForceProviderConfig _gravityForceProviderConfig;
        [SerializeField] private ForceProviderConfig _explosionForceProviderConfig;
        [SerializeField, InlineProperty] private CameraFieldProviderConfig _cameraFieldProviderConfig;
        [SerializeField] private ColliderFieldCatcherConfig _colliderFieldCatcherConfig;
        [SerializeField] private RandomProviderConfig _randomProviderConfig;
        [SerializeField, InlineProperty] private ObjectsContainerConfig _objectsContainerConfig;
        [SerializeField] private ExplosionProviderConfig _explosionProviderConfig;
        [SerializeField] private PhysicsObjectSpawnerConfig _physicsObjectSpawnerConfig;
        [SerializeField] private ScaleProviderConfig _scaleProviderConfig;
        [SerializeField] private LifecycleManagerConfig _lifecycleManagerConfig;
        [SerializeField] private ClickObjectDestroyerConfig _clickObjectDestroyerConfig;
        [SerializeField] private GyroscopeGravityChangerConfig _gyroscopeGravityChangerConfig;
        [SerializeField, InlineProperty] private EffectObjectsContainerConfig _effectObjectsContainerConfig;
        [SerializeField] private SplitSpriteEffectProviderConfig _splitSpriteEffectProviderConfig;
        
        public PhysicsEngineConfig PhysicsEngineConfig => _physicsEngineConfig;
        public ForceProviderConfig GravityForceProviderConfig => _gravityForceProviderConfig;
        public ForceProviderConfig ExplosionForceProviderConfig => _explosionForceProviderConfig;
        public CameraFieldProviderConfig CameraFieldProviderConfig => _cameraFieldProviderConfig;
        public ColliderFieldCatcherConfig ColliderFieldCatcherConfig => _colliderFieldCatcherConfig;
        public RandomProviderConfig RandomProviderConfig => _randomProviderConfig;
        public ObjectsContainerConfig ObjectsContainerConfig => _objectsContainerConfig;
        public ExplosionProviderConfig ExplosionProviderConfig => _explosionProviderConfig;
        public PhysicsObjectSpawnerConfig PhysicsObjectSpawnerConfig => _physicsObjectSpawnerConfig;
        public ScaleProviderConfig ScaleProviderConfig => _scaleProviderConfig;
        public LifecycleManagerConfig LifecycleManagerConfig => _lifecycleManagerConfig;
        public ClickObjectDestroyerConfig ClickObjectDestroyerConfig => _clickObjectDestroyerConfig;
        public GyroscopeGravityChangerConfig GyroscopeGravityChangerConfig => _gyroscopeGravityChangerConfig;
        public EffectObjectsContainerConfig EffectObjectsContainerConfig => _effectObjectsContainerConfig;
        public SplitSpriteEffectProviderConfig SplitSpriteEffectProviderConfig => _splitSpriteEffectProviderConfig;
    }
}