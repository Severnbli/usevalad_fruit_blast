using _Project.Scripts.Common.Repositories;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using UnityEngine;

namespace _Project.Scripts.Features.FeatureCore
{
    public static class FeatureContextExtensions
    {
        public static TFeature AddFeature<TFeature>(this Context<BaseFeature> context, TFeature feature) 
            where TFeature : BaseFeature
        {
            context.AddComponent(feature);
            
            feature.SetContext(context);
            feature.Init();
            
            return feature;
        }

        public static TFeature AddFeatureWithConfig<TFeature, TConfig>(this Context<BaseFeature> context, 
            TFeature feature, TConfig config) 
            where TFeature : BaseFeature
            where TConfig : IFeatureConfig
        {
            feature = context.AddFeature(feature);

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