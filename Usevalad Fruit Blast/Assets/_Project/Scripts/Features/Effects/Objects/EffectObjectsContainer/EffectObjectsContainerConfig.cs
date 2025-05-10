using System;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects.EffectObjectsContainer
{
    [Serializable]
    public class EffectObjectsContainerConfig : IFeatureConfig
    {
        [SerializeField] private Transform _effectObjectsContainerTransform;
        [SerializeField] private float _deleteFieldOffset = 2f;
        
        public Transform EffectObjectsContainerTransform => _effectObjectsContainerTransform;
        public float DeleteFieldOffset => _deleteFieldOffset;
    }
}