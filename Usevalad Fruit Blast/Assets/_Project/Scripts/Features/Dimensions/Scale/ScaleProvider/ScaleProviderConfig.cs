using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Dimensions.Scale.ScaleProvider
{
    [Serializable]
    public class ScaleProviderConfig : IFeatureConfig
    {
        [SerializeField] private Vector2 _benchmarkResolution = new Vector2(1080, 1920);
        
        public Vector2 BenchmarkResolution => _benchmarkResolution;
    }
}