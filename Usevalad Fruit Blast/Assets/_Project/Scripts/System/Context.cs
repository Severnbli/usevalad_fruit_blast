using System.Collections.Generic;
using _Project.Scripts.Common;
using UnityEngine;

namespace _Project.Scripts.System
{
    public static class Context
    {
        public static GameObject Container { get; private set; }
        public static List<BaseFeature> OtherScopeFeatures { get; private set; }
        
        private static readonly string _containerName = "Container";

        public static void SetupContext(out GameObject container, out List<BaseFeature> otherScopeFeatures)
        {
            container = SetupContainer();
            otherScopeFeatures = SetupOtherScopeFeatures();
        }
        
        public static GameObject SetupContainer()
        {
            Container = new(_containerName);
            return Container;
        }

        public static List<BaseFeature> SetupOtherScopeFeatures()
        {
            OtherScopeFeatures = new();
            return OtherScopeFeatures;
        }

        public static void ClearContext()
        {
            ClearContainer();
            ClearOtherScopeFeatures();
        }

        public static void ClearContainer()
        {
            Object.Destroy(Container);
            Container = null;
        }

        public static void ClearOtherScopeFeatures()
        {
            OtherScopeFeatures.Clear();
            OtherScopeFeatures = null;
        }

        public static bool TryGetComponentFromContainer<T>(out T feature) where T : BaseFeature
        {
            feature = null;

            if (Container == null)
            {
                return false;
            }
            
            feature = Container.GetComponent<T>();
            
            return feature != null;
        }

        public static bool TryGetComponentsFromContainer<T>(out T[] features) where T : BaseFeature
        {
            features = null;

            if (Container == null)
            {
                return false;
            }
            
            features = Container.GetComponents<T>();
            
            return true;
        }

        public static bool TryGetComponentFromOtherScope<T>(out T feature) where T : BaseFeature
        {
            feature = null;

            if (OtherScopeFeatures == null)
            {
                return false;
            }

            foreach (var otherScopeFeature in OtherScopeFeatures)
            {
                if (otherScopeFeature is not T otherScopeFeatureAsT)
                {
                    continue;
                }
                
                feature = otherScopeFeatureAsT;
                return true;
            }
            
            return false;
        }
        
        public static bool TryGetComponentsFromOtherScope<T>(out T[] features) where T : BaseFeature
        {
            features = null;
            var foundFeatures = new List<T>();

            if (OtherScopeFeatures == null)
            {
                return false;
            }

            foreach (var otherScopeFeature in OtherScopeFeatures)
            {
                if (otherScopeFeature is not T otherScopeFeatureAsT)
                {
                    continue;
                }
                
                foundFeatures.Add(otherScopeFeatureAsT);
            }
            
            features = foundFeatures.ToArray();
            return true;
        }
    }
}