using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Figures;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionFinder;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer
{
    public class ObjectsContainer : BaseFeature, IConfigurableFeature<ObjectsContainerConfig>
    {
        public ObjectsContainerConfig ObjectsContainerConfig { get; private set; }
        public List<ContainerableObject> ContainerableObjects { get; protected set; } = new();

        public void Configure(ObjectsContainerConfig objectsContainerConfig)
        {
            ObjectsContainerConfig = objectsContainerConfig;
        }
        
        public float GetTotalArea()
        {
            var totalArea = ContainerableObjects.Sum(x => x.GetArea());
            
            return totalArea;
        }

        public Transform GetObjectContainerTransform()
        {
            return ObjectsContainerConfig.ObjectsContainerTransform;
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