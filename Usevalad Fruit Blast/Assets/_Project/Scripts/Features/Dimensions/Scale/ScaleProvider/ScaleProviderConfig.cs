using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Dimensions.Scale.ScaleProvider
{
    [Serializable]
    public class ScaleProviderConfig : IFeatureConfig
    {
        [SerializeField] private Vector2 _benchmarkSize = new Vector2(3.5f, 7.5f);
        
        public Vector2 BenchmarkSize => _benchmarkSize;
    }
}