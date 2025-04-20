using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider
{
    [Serializable]
    public class CameraFieldProviderConfig : IFeatureConfig
    {
        [SerializeField] private Camera _targetCamera;
        
        public Camera TargetCamera { get => _targetCamera; set => _targetCamera = value; }
    }
}