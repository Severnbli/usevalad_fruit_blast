using System;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider;
using _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Forces;
using _Project.Scripts.Features.Random;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.System.SystemConfigurator
{
    [Serializable]
    public class SystemConfig
    {
        [SerializeField] private PhysicsEngineConfig _physicsEngineConfig;
        [SerializeField, InlineProperty] private ForceProviderConfig _gravityForceProviderConfig;
        [SerializeField, InlineProperty] private CameraFieldProviderConfig _cameraFieldProviderConfig;
        [SerializeField] private FieldCatcherConfig _fieldCatcherConfig;
        [SerializeField, InlineProperty] private RandomProviderConfig _randomProviderConfig;
        [SerializeField] private PhysicsObjectSpawnerConfig _physicsObjectSpawnerConfig;
        
        public PhysicsEngineConfig PhysicsEngineConfig => _physicsEngineConfig;
        public ForceProviderConfig GravityForceProviderConfig => _gravityForceProviderConfig;
        public CameraFieldProviderConfig CameraFieldProviderConfig => _cameraFieldProviderConfig;
        public FieldCatcherConfig FieldCatcherConfig => _fieldCatcherConfig;
        public RandomProviderConfig RandomProviderConfig => _randomProviderConfig;
        public PhysicsObjectSpawnerConfig PhysicsObjectSpawnerConfig => _physicsObjectSpawnerConfig;
    }
}