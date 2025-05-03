using System;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Forces;
using UnityEngine;

namespace _Project.Scripts.System.SystemConfigurator
{
    [Serializable]
    public class SystemConfig
    {
        [SerializeField] private PhysicsEngineConfig _physicsEngineConfig;
        [SerializeField] private ForceProviderConfig _gravityForceProviderConfig;
        [SerializeField] private CameraFieldProviderConfig _cameraFieldProviderConfig;
        [SerializeField] private FieldCatcherConfig _fieldCatcherConfig;
        
        public PhysicsEngineConfig PhysicsEngineConfig => _physicsEngineConfig;
        public ForceProviderConfig GravityForceProviderConfig => _gravityForceProviderConfig;
        public CameraFieldProviderConfig CameraFieldProviderConfig => _cameraFieldProviderConfig;
        public FieldCatcherConfig FieldCatcherConfig => _fieldCatcherConfig;
    }
}