using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Physics.Dynamic;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Objects
{
    public class ContainerableObject : MonoBehaviour
    {
        [SerializeField] private int _id;
        
        private DynamicBody _dynamicBody;
        private ObjectsContainer.ObjectsContainer _objectContainer;
        
        public int Id { get => _id; set => _id = value; }

        private void Start()
        {
            if (!ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                return;
            }

            if (!systemCoordinator.Context.TryGetComponentFromContainer(out _objectContainer))
            {
                return;
            }
            
            _objectContainer.ContainerableObjects.Add(this);
            
            _dynamicBody = GetComponent<DynamicBody>();
        }

        private void OnDestroy()
        {
            if (_objectContainer == null)
            {
                return;
            }
            
            _objectContainer.ContainerableObjects.Remove(this);
        }

        public float GetArea()
        {
            var area = 0f;

            if (_dynamicBody != null)
            {
                foreach (var collider in _dynamicBody.Colliders)
                {
                    area += collider.GetFigure().GetArea();
                }
            }
            
            return area;
        }
    }
}