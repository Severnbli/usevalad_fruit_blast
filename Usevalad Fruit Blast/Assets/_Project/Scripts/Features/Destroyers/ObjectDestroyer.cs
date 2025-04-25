using System.Collections.Generic;
using _Project.Scripts.Features.Common;

namespace _Project.Scripts.Features.Destroyers
{
    public abstract class ObjectDestroyer : BaseFeature
    {
        public List<DestroyableObject.DestroyableObject> DestroyableObjects = new();
        
        public override void Init(IFeatureConfig config) {}
    }
}