using UnityEngine;

namespace _Project.Scripts.Common.Finders
{
    public class ObjectFinder
    {
        public static bool TryFindObjectByType<T>(out T foundObject, bool notifyOnFail = true) where T : Object
        {
            foundObject = default;

            foundObject = Object.FindFirstObjectByType<T>();

            if (foundObject != null)
            {
                return true;
            }
            
            if (notifyOnFail)
            {
                Debug.LogError($"Object {typeof(T).Name} could not be found!");
            }
            
            return false;
        }
    }
}