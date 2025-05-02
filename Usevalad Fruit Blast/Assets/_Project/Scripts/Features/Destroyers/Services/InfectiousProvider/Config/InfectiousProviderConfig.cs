using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.Services.InfectiousProvider.Config
{
    [Serializable]
    public class InfectiousProviderConfig : IFeatureConfig
    {
        [SerializeField] private float _infectiousDistance = 0.5f;
        
        public float InfectiousDistance => _infectiousDistance;
    }
}