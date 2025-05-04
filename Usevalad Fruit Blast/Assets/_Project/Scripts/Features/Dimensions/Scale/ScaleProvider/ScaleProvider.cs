using System;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider;
using _Project.Scripts.System;
using _Project.Scripts.System.Logs.Logger;
using UnityEngine;

namespace _Project.Scripts.Features.Dimensions.Scale.ScaleProvider
{
    public class ScaleProvider : BaseFeature, IConfigurableFeature<ScaleProviderConfig>
    {
        [SerializeField] private ScaleProviderConfig _scaleProviderConfig;
        [SerializeField] private FieldCatcher _fieldCatcher;

        private Vector2 _lastSize = Vector2.zero;
        
        public ScaleProviderConfig ScaleProviderConfig => _scaleProviderConfig;
        public FieldCatcher FieldCatcher => _fieldCatcher;
        
        public float Scale { get; private set; }
        public event Action<float> OnChangeScale;

        private void FixedUpdate()
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

            if (!Context.TryGetComponentFromContainer(out _fieldCatcher))
            {
                LogManager.RegisterLogMessage(LogManager.LogType.Error, LogMessages.DependencyNotFound(
                    GetType().ToString(), _fieldCatcher.GetType().ToString()));
            }
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