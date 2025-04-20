using UnityEngine;

namespace _Project.Scripts.Features.Common
{
    public static class FeatureFactory
    {
        public static T AddFeature<T>(this GameObject container, IFeatureConfig config) where T : BaseFeature
        {
            var feature = container.AddComponent<T>();
            feature.Init(config);
            return feature;
        }
    }
}