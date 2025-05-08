using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Engine;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Forces
{
    public abstract class ForceProvider : BaseFeature, IConfigurableFeature<ForceProviderConfig>
    {
        public ForceProviderConfig ForceProviderConfig { get; private set; }
        private PhysicsEngine _physicsEngine;
        
        public Vector2 GetForce()
        {
            return ForceProviderConfig.Factor * ForceProviderConfig.Direction;
        }
        
        public abstract Vector2 GetForceByDynamicBody(DynamicBody dynamicBody);

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _physicsEngine);
            
            _physicsEngine.ForceProviders.Add(this);
        }

        public void Configure(ForceProviderConfig forceProviderConfig)
        {
            ForceProviderConfig = forceProviderConfig;
        }
    }
}