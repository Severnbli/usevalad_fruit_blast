using System;
using UnityEngine;

namespace _Project.Scripts.Features.Common
{
    public static class FeatureFactory
    {
        public static TFeature AddFeature<TFeature>(this GameObject container) where TFeature : BaseFeature
        {
            var feature = container.AddComponent<TFeature>();
            feature.Init();
            return feature;
        }

        public static TFeature AddFeatureWithConfig<TFeature, TConfig>(this GameObject container, TConfig config) 
            where TFeature : BaseFeature
            where TConfig : IFeatureConfig
        {
            var feature = container.AddFeature<TFeature>();

            if (feature is not IConfigurableFeature<TConfig> configurableFeature)
            {
                Debug.LogError($"Check SystemConfigurator with feature {typeof(TFeature).Name} configuration!");
            }
            else
            {
                configurableFeature.Configure(config);
            }
            
            return feature;
        }
    }
}