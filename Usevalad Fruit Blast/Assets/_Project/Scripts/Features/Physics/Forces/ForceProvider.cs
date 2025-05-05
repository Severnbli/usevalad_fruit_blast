using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Engine;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Forces
{
    public abstract class ForceProvider : BaseFeature, IConfigurableFeature<ForceProviderConfig>
    {
        private ForceProviderConfig _forceProviderConfig;
        private PhysicsEngine _physicsEngine;
        
        public ForceProviderConfig ForceProviderConfig => _forceProviderConfig;
        
        public Vector2 GetForce()
        {
            return _forceProviderConfig.Factor * _forceProviderConfig.Direction;
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
            _forceProviderConfig = forceProviderConfig;
        }
    }
}