using System;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer
{
    [Serializable]
    public class ObjectsContainerConfig : IFeatureConfig
    {
        [SerializeField] private Transform _objectsContainerTransform;
        [SerializeField] private float _deleteFieldOffset = 1f;
        
        public Transform ObjectsContainerTransform => _objectsContainerTransform;
        public float DeleteFieldOffset => _deleteFieldOffset;
    }
}