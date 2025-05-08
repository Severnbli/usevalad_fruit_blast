using System.Collections.Generic;
using _Project.Scripts.Features.Physics.Dynamic;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Forces.ExplosionForceProvider
{
    public class ExplosionForceProvider : ForceProvider
    {
        private readonly Dictionary<DynamicBody, Vector2> _explosionData = new();
        
        public override Vector2 GetForceByDynamicBody(DynamicBody dynamicBody)
        {
            if (!_explosionData.TryGetValue(dynamicBody, out var forcePercentageVector))
            {
                return Vector2.zero;
            }

            return forcePercentageVector * _forceProviderConfig.Factor;
        }

        public bool TryAddExplosionData(DynamicBody dynamicBody, Vector2 distanceVector)
        {
            return _explosionData.TryAdd(dynamicBody, distanceVector);
        }
    }
}