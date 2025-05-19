using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Field.FieldProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer
{
    public class ObjectsContainer : BaseFeature, IConfigurableFeature<ObjectsContainerConfig>, 
        IUpdatableFeature
    {
        private FieldProvider _fieldProvider;    
        
        public ObjectsContainerConfig ObjectsContainerConfig { get; private set; }
        public List<ContainerableObject> ContainerableObjects { get; protected set; } = new();

        public override void Init()
        {
            base.Init();
            
            Context.TryGetComponentFromContainer(out _fieldProvider);
        }

        public void Configure(ObjectsContainerConfig objectsContainerConfig)
        {
            ObjectsContainerConfig = objectsContainerConfig;
        }
        
        public void Update()
        {
            for (var i = ContainerableObjects.Count - 1; i >= 0; i--)
            {
                if (ContainerableObjects[i] == null)
                {
                    ContainerableObjects.RemoveAt(i);
                    continue;
                }
                
                if (_fieldProvider.IsObjectOutOfScreen(ContainerableObjects[i].gameObject, ObjectsContainerConfig.DeleteFieldOffset))
                {
                    return;
                }
                
                Object.Destroy(ContainerableObjects[i].gameObject);
                ContainerableObjects.RemoveAt(i);
            }
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