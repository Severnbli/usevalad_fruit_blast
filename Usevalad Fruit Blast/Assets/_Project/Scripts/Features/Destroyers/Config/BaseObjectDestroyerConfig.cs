using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.Config
{
    [Serializable]
    public class BaseObjectDestroyerConfig : IFeatureConfig
    {
        [SerializeField] private float _destroyDelay;
        
        public float DestroyDelay => _destroyDelay;
    }
}