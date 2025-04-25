using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.System
{
    public static class Context
    {
        private static GameObject _container;
        private static HashSet<MonoBehaviour> _components;
        
        public static GameObject Container => _container;
        public static HashSet<MonoBehaviour> Components => _components;
        public static readonly string ContainerName = "Container";

        public static GameObject SetupContainer()
        {
            _container = new(ContainerName);
            return _container;
        }

        public static HashSet<MonoBehaviour> SetupComponents()
        {
            _components = new();
            return _components;
        }

        public static bool TryGetFromComponents<T>(out T resultComponent) where T : MonoBehaviour
        {
            resultComponent = null;
            
            if (_components == null)
            {
                return false;
            }
            
            foreach (var component in _components)
            {
                if (component is T componentAsT)
                {
                    resultComponent = componentAsT;
                    return true;
                }
            }
            
            return false;
        }

        public static bool TryGetComponentFromContainer<T>(out T resultComponent) where T : MonoBehaviour
        {
            resultComponent = null;

            if (_container == null)
            {
                return false;
            }
            
            resultComponent = _container.GetComponent<T>();
            return resultComponent != null;
        }
        
        public static bool TryGetComponentsFromContainer<T>(out T[] resultComponents) where T : MonoBehaviour
        {
            resultComponents = null;

            if (_container == null)
            {
                return false;
            }
            
            resultComponents = _container.GetComponents<T>();
            return true;
        }

        public static void ClearContainer()
        {
            _container = null;
        }

        public static void ClearComponents()
        {
            _components = null;
        }
    }
}