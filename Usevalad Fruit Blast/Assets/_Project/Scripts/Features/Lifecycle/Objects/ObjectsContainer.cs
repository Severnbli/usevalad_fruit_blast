using System.Collections.Generic;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Objects
{
    public class ObjectsContainer : BaseFeature
    {
        public List<GameObject> ContainedObjects { get; protected set; } = new();
    }
}