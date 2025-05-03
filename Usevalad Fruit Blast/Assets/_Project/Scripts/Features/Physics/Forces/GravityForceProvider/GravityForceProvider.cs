using _Project.Scripts.Features.Physics.Dynamic;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Forces.GravityForceProvider
{
    public class GravityForceProvider : ForceProvider
    {
        public override Vector2 GetForceByDynamicBody(DynamicBody dynamicBody)
        {
            if (!dynamicBody.UseGravity)
            {
                return Vector2.zero;
            }
            
            return dynamicBody.GravityFactor * GetForce();
        }
    }
}