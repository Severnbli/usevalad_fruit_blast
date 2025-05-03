using System;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldProvider;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Dimensions.Scale.ScaleProvider
{
    public class ScaleProvider : BaseFeature, IConfigurableFeature<ScaleProviderConfig>
    {
        [SerializeField] private ScaleProviderConfig _scaleProviderConfig;
        
        public ScaleProviderConfig ScaleProviderConfig => _scaleProviderConfig;
        
        public float Scale { get; protected set; }
        public event Action<float> OnChangeScale;

        public void Configure(ScaleProviderConfig scaleProviderConfig)
        {
            _scaleProviderConfig = scaleProviderConfig;
        }

        public void ChangeScale(Vector2 resolution)
        {
            
        }
    }
}