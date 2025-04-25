using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.ClickObjectDestroyer.Config
{
    [Serializable]
    public class ClickObjectDestroyerConfig : IFeatureConfig
    {
        [SerializeField] private float _destroyDistance;
        
        public float DestroyDistance => _destroyDistance;
    }
}