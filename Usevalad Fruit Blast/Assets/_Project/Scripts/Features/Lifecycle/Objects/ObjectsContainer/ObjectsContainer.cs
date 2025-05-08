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

        public List<IPhysicsFigure> GetContainerablePhysicsFigures()
        {
            List<IPhysicsFigure> physicsFigures = new();

            foreach (var containerableObject in ContainerableObjects)
            {
                var baseColliders = containerableObject.GetComponents<BaseCollider>();

                foreach (var baseCollider in baseColliders)
                {
                    physicsFigures.Add(baseCollider.GetFigure());
                }
            }
            
            return physicsFigures;
        }

        public List<ContainerableObject> GetContainerableObjectsThatCollideWith(IPhysicsFigure figure)
        {
            List<ContainerableObject> containerableObjects = new();
            
            foreach (var containerableObject in ContainerableObjects)
            {
                if (!containerableObject.TryGetComponent(out BaseCollider collider))
                {
                    continue;
                }
                
                if (CollisionFinder.IsFiguresCollide(figure, collider.GetFigure()))
                {
                    containerableObjects.Add(containerableObject);
                }
            }
            
            return containerableObjects;
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