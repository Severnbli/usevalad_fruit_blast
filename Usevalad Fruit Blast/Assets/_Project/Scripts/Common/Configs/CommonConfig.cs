using System;
using _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider;
using _Project.Scripts.Features.Gizmo.GizmoProvider.BaseGizmoProvider;
using _Project.Scripts.Features.Physics.Forces.GravityForce;
using UnityEngine;

namespace _Project.Scripts.Common.Configs
{
    [Serializable]
    public class CommonConfig : MonoBehaviour
    {
        [SerializeField] private CameraFieldProviderConfig _cameraFieldProviderConfig;
        [SerializeField] private ColliderFieldCatcherConfig _colliderFieldCatcherConfig;
        [SerializeField] private BaseGizmoProviderConfig _baseGizmoProviderConfig;
        [SerializeField] private GravityForceProviderConfig _gravityForceProviderConfig;
        
        public CameraFieldProviderConfig CameraFieldProviderConfig => _cameraFieldProviderConfig;
        public ColliderFieldCatcherConfig ColliderFieldCatcherConfig => _colliderFieldCatcherConfig;
        public BaseGizmoProviderConfig BaseGizmoProviderConfig => _baseGizmoProviderConfig;
        public GravityForceProviderConfig GravityForceProviderConfig => _gravityForceProviderConfig;
    }
}