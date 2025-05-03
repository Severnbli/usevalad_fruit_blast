using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Random
{
    [Serializable]
    public class RandomProviderConfig : IFeatureConfig
    {
        [SerializeField] private int _seed;
        
        public int Seed => _seed;
    }
}