using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer
{
    [Serializable]
    public class ObjectsContainerConfig : IFeatureConfig
    {
        [SerializeField] private Transform _objectsContainerTransform;
        
        public Transform ObjectsContainerTransform => _objectsContainerTransform;
    }
}