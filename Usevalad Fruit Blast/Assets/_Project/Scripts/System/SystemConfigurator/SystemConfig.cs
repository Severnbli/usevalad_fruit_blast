using System;
using _Project.Scripts.Features.Dimensions.Scale.ScaleProvider;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider;
using _Project.Scripts.Features.Lifecycle.Destroyers.ClickObjectDestroyer;
using _Project.Scripts.Features.Lifecycle.LifecycleManager;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Forces;
using _Project.Scripts.Features.Random;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.System.SystemConfigurator
{
    [Serializable]
    public class SystemConfig
    {
        [SerializeField] private PhysicsEngineConfig _physicsEngineConfig;
        [SerializeField, InlineProperty] private ForceProviderConfig _gravityForceProviderConfig;
        [SerializeField, InlineProperty] private CameraFieldProviderConfig _cameraFieldProviderConfig;
        [SerializeField] private ColliderFieldCatcherConfig _colliderFieldCatcherConfig;
        [SerializeField, InlineProperty] private RandomProviderConfig _randomProviderConfig;
        [SerializeField, InlineProperty] private ObjectsContainerConfig _objectsContainerConfig;
        [SerializeField] private PhysicsObjectSpawnerConfig _physicsObjectSpawnerConfig;
        [SerializeField, InlineProperty] private ScaleProviderConfig _scaleProviderConfig;
        [SerializeField] private LifecycleManagerConfig _lifecycleManagerConfig;
        [SerializeField, InlineProperty] private ClickObjectDestroyerConfig _clickObjectDestroyerConfig;
        
        public PhysicsEngineConfig PhysicsEngineConfig => _physicsEngineConfig;
        public ForceProviderConfig GravityForceProviderConfig => _gravityForceProviderConfig;
        public CameraFieldProviderConfig CameraFieldProviderConfig => _cameraFieldProviderConfig;
        public ColliderFieldCatcherConfig ColliderFieldCatcherConfig => _colliderFieldCatcherConfig;
        public RandomProviderConfig RandomProviderConfig => _randomProviderConfig;
        public ObjectsContainerConfig ObjectsContainerConfig => _objectsContainerConfig;
        public PhysicsObjectSpawnerConfig PhysicsObjectSpawnerConfig => _physicsObjectSpawnerConfig;
        public ScaleProviderConfig ScaleProviderConfig => _scaleProviderConfig;
        public LifecycleManagerConfig LifecycleManagerConfig => _lifecycleManagerConfig;
        public ClickObjectDestroyerConfig ClickObjectDestroyerConfig => _clickObjectDestroyerConfig;
    }
}