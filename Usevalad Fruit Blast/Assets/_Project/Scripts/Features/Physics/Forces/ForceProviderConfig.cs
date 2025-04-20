using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Forces
{
    [Serializable]
    public class ForceProviderConfig : IFeatureConfig
    {
        [SerializeField] private float _factor;
        [SerializeField] private Vector2 _direction;
        
        public float Factor => _factor;
        public Vector2 Direction => _direction;
    }
}