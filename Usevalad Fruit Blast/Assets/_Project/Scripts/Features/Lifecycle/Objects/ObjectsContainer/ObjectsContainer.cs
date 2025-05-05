using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Colliders;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer
{
    public class ObjectsContainer : BaseFeature, IConfigurableFeature<ObjectsContainerConfig>
    {
        [SerializeField] private ObjectsContainerConfig _objectsContainerConfig;
        
        public ObjectsContainerConfig ObjectsContainerConfig => _objectsContainerConfig;
        public List<ContainerableObject> ContainerableObjects { get; protected set; } = new();

        public void Configure(ObjectsContainerConfig objectsContainerConfig)
        {
            _objectsContainerConfig = objectsContainerConfig;
        }
        
        public float GetTotalArea()
        {
            var totalArea = ContainerableObjects.Sum(x => x.GetArea());
            
            return totalArea;
        }

        public Transform GetObjectContainerTransform()
        {
            return _objectsContainerConfig.ObjectsContainerTransform;
        }

        public List<T> GetComponentsFromContainerableObjects<T>()
        {
            var list = new List<T>();
            
            foreach (var containerableObject in ContainerableObjects)
            {
                list.AddRange(containerableObject.GetComponents<T>());
            }
            
            return list;
        }
    }
}