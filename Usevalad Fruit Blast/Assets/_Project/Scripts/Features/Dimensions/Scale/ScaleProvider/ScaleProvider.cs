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
        [SerializeField] private FieldProvider _fieldProvider;
        
        public ScaleProviderConfig ScaleProviderConfig => _scaleProviderConfig;
        public FieldProvider FieldProvider => _fieldProvider;
        
        public float Scale { get; protected set; }
        public event Action<float> OnChangeScale;
        
        public override void Init()
        {
            base.Init();

            if (!Context.TryGetComponentFromContainer(out FieldProvider fieldProvider))
            {
                Debug.LogError("Check system priority setup: field provider must be earlier than scale provider!");
                return;
            }
            
            _fieldProvider = fieldProvider;
        }

        public void Configure(ScaleProviderConfig scaleProviderConfig)
        {
            _scaleProviderConfig = scaleProviderConfig;
        }

        public void ChangeScale(Vector2 resolution)
        {
            
        }
    }
}