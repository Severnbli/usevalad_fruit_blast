using UnityEngine;

namespace _Project.Scripts.Common
{
    public static class FeatureFactory
    {
        public static T AddFeature<T>(this GameObject container) where T : BaseFeature
        {
            var feature = container.AddComponent<T>();
            feature.Init();
            return feature;
        }
    }
}