using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Random.Config
{
    [Serializable]
    public class RandomProviderConfig : IFeatureConfig
    {
        [SerializeField] private int _seed;
        
        public int Seed { get => _seed; set => _seed = value; }
    }
}