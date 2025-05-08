using System;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Explosions.ExplosionProvider
{
    [Serializable]
    public class ExplosionProviderConfig : IFeatureConfig
    {
        [SerializeField] private float _maxAffectedDistance = 3f;
        
        public float MaxAffectedDistance => _maxAffectedDistance;
    }
}