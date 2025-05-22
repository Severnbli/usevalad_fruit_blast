using UnityEngine;

namespace _Project.Scripts.Common.Serializers
{
    public static class JsonSerializer
    {
        private class Wrapper<T> {
            public T[] Items;
        }
        
        public static T ObjectFromJson<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
        
        public static T[] CollectionFromJson<T>(string json)
        {
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ObjectToJson(object item)
        {
            return JsonUtility.ToJson(item);
        }
        
        public static string CollectionToJson<T>(T[] items, bool prettyPrint = false)
        {
            var wrapper = new Wrapper<T> { Items = items };
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }
    }
}