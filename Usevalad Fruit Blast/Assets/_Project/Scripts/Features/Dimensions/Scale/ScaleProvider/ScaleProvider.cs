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

        private Vector2 _lastScale = Vector2.zero;
        
        public ScaleProviderConfig ScaleProviderConfig => _scaleProviderConfig;
        
        public float Scale { get; protected set; }
        public event Action<float> OnChangeScale;

        private void FixedUpdate()
        {
            if (!IsResolutionChanged())
            {
                return;
            }
            
            ChangeScale();
        }
        
        public void Configure(ScaleProviderConfig scaleProviderConfig)
        {
            _scaleProviderConfig = scaleProviderConfig;
        }

        public bool IsResolutionChanged()
        {
            return !_lastScale.x.Equals(Screen.width) || !_lastScale.y.Equals(Screen.height);
        }

        public void ChangeScale()
        {
            _lastScale.x = Screen.width;
            _lastScale.y = Screen.height;

            var widthDelta = _scaleProviderConfig.BenchmarkResolution.x - _lastScale.x;
            var heightDelta = _scaleProviderConfig.BenchmarkResolution.y - _lastScale.y;

            if (widthDelta < heightDelta)
            {
                Scale = (_scaleProviderConfig.BenchmarkResolution.x - widthDelta) 
                        / _scaleProviderConfig.BenchmarkResolution.x; 
            }
            else
            {
                Scale = (_scaleProviderConfig.BenchmarkResolution.y - heightDelta) 
                        / _scaleProviderConfig.BenchmarkResolution.y;
            }
            
            OnChangeScale?.Invoke(Scale);
        }
    }
}