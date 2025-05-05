using System;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Field.FieldCatcher;
using UnityEngine;

namespace _Project.Scripts.Features.Dimensions.Scale.ScaleProvider
{
    public class ScaleProvider : BaseFeature, IConfigurableFeature<ScaleProviderConfig>, IFixedUpdatableFeature
    {
        protected ScaleProviderConfig _scaleProviderConfig;
        protected FieldCatcher _fieldCatcher;

        private Vector2 _lastSize = Vector2.zero;
        
        public float Scale { get; private set; }
        public event Action<float> OnChangeScale;

        public void FixedUpdate()
        {
            var fieldCatcherSize = _fieldCatcher.GetCatcherSize();
            
            if (!IsSizeChanged(fieldCatcherSize))
            {
                return;
            }
            
            ChangeScale(fieldCatcherSize);
        }

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _fieldCatcher);
        }
        
        public void Configure(ScaleProviderConfig scaleProviderConfig)
        {
            _scaleProviderConfig = scaleProviderConfig;
        }

        public bool IsSizeChanged(Vector2 fieldCatcherSize)
        {
            return !_lastSize.x.Equals(fieldCatcherSize.x) || !_lastSize.y.Equals(fieldCatcherSize.y);
        }

        public void ChangeScale(Vector2 fieldCatcherSize)
        {
            _lastSize = fieldCatcherSize;
            
            var widthDelta = _scaleProviderConfig.BenchmarkSize.x - _lastSize.x;
            var heightDelta = _scaleProviderConfig.BenchmarkSize.y - _lastSize.y;

            if (Mathf.Abs(widthDelta) > Mathf.Abs(heightDelta))
            {
                Scale = _lastSize.x / _scaleProviderConfig.BenchmarkSize.x;
            }
            else
            {
                Scale = _lastSize.y / _scaleProviderConfig.BenchmarkSize.y;
            }
            
            OnChangeScale?.Invoke(Scale);
        }
    }
}