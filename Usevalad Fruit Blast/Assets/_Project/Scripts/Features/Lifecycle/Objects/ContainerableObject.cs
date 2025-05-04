using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.System;
using _Project.Scripts.System.Logs.Logger;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Objects
{
    public class ContainerableObject : MonoBehaviour
    {
        [SerializeField] private int _id;
        
        private DynamicBody _dynamicBody;
        
        public int Id { get => _id; set => _id = value; }

        private void OnEnable()
        {
            if (!Context.TryGetComponentFromContainer(out ObjectsContainer objectContainer))
            {
                LogManager.RegisterLogMessage(LogManager.LogType.Error, LogMessages.DependencyNotFound(
                    GetType().ToString(), objectContainer.GetType().ToString()));
                return;
            }
            
            objectContainer.ContainerableObjects.Add(this);
        }

        private void Start()
        {
            _dynamicBody = GetComponent<DynamicBody>();
        }

        private void OnDisable()
        {
            if (!Context.TryGetComponentFromContainer(out ObjectsContainer objectContainer))
            {
                return;
            }
            
            objectContainer.ContainerableObjects.Remove(this);
        }

        public float GetArea()
        {
            var area = 0f;

            if (_dynamicBody != null)
            {
                foreach (var collider in _dynamicBody.Colliders)
                {
                    area += collider.GetAreaWithScale();
                }
                
            }
            
            return area;
        }
    }
}