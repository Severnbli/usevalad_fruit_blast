using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Features.Common;

namespace _Project.Scripts.Features.Lifecycle.Objects
{
    public class ObjectsContainer : BaseFeature
    {
        public List<ContainerableObject> ContainerableObjects { get; protected set; } = new();

        public float GetTotalArea()
        {
            var totalArea = ContainerableObjects.Sum(x => x.GetArea());
            
            return totalArea;
        }
    }
}