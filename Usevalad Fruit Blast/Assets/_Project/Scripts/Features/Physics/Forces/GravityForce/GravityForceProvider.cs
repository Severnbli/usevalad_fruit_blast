using _Project.Scripts.Features.Physics.Dynamic;

namespace _Project.Scripts.Features.Physics.Forces.GravityForce
{
    public class GravityForceProvider : ForceProvider
    {
        public override void ApplyForceToDynamicBody(DynamicBody dynamicBody, float deltaTime)
        {
            if (!dynamicBody.UseGravity)
            {
                return;
            }
            
            dynamicBody.Velocity += dynamicBody.GravityFactor * GetForce(deltaTime);
        }
    }
}