using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Common.Repositories
{
    public class Context<TContainer>
    {
        public List<TContainer> Container { get; private set; }

        public Context()
        {
            var container = new List<TContainer>();
            
            Setup(container);
        }
        
        public Context(List<TContainer> container)
        {
            Setup(container);
        }

        private void Setup(List<TContainer> container)
        {
            Container = container;
        }

        public void Clear()
        {
            Container.Clear();
        }

        public void Update()
        {
            Clear();
            Setup(new List<TContainer>());
        }

        public T AddComponent<T>(T component) where T : TContainer
        {
            Container.Add(component);
            
            return component;
        }
            
        public bool TryGetComponentFromContainer<T>(out T component, bool notifyOnFail = true) where T : TContainer
        {
            component = default;

            if (!TryGetContainer(out var container, notifyOnFail))
            {
                return false;
            }

            component = container.OfType<T>().FirstOrDefault();

            if (component == null && notifyOnFail)
            {
                Debug.LogWarning($"Component {typeof(T)} not found in container!");
            }
            
            return component != null;
        }

        public bool TryGetComponentsFromContainer<T>(out T[] components, bool notifyOnFail = true) where T : TContainer
        {
            components = null;

            if (!TryGetContainer(out var container, notifyOnFail))
            {
                return false;
            }
            
            components = container.OfType<T>().ToArray();
            
            if (components.Length == 0 && notifyOnFail)
            {
                Debug.LogWarning($"Components {typeof(T)} not found in container!");
            }
            
            return true;
        }

        public bool TryGetContainer(out List<TContainer> container, bool notifyOnFail = true)
        {
            container = Container;

            if (container != null)
            {
                return true;
            }
            
            if (notifyOnFail)
            {
                Debug.LogError("Container not found!");
            }
                
            return false;
        }
    }
}