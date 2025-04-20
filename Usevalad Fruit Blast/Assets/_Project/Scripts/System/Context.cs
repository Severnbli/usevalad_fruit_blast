using UnityEngine;

namespace _Project.Scripts.System
{
    public static class Context
    {
        private static GameObject _container; 
        
        public static GameObject Container => _container;
        public static string ContainerName = "Container";

        public static GameObject SetupContainer()
        {
            _container = new(ContainerName);
            return _container;
        }

        public static void ClearContainer()
        {
            _container = null;
        }
    }
}