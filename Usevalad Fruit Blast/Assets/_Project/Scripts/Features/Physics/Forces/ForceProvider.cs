using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.System;
using _Project.Scripts.System.Logs.Logger;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Forces
{
    public abstract class ForceProvider : BaseFeature, IConfigurableFeature<ForceProviderConfig>
    {
        [SerializeField] private ForceProviderConfig _forceProviderConfig;
        
        public ForceProviderConfig ForceProviderConfig => _forceProviderConfig;
        
        public Vector2 GetForce()
        {
            return _forceProviderConfig.Factor * _forceProviderConfig.Direction;
        }
        
        public abstract Vector2 GetForceByDynamicBody(DynamicBody dynamicBody);

        public override void Init()
        {
            base.Init();

            if (!Context.TryGetComponentFromContainer(out PhysicsEngine physicsEngine))
            {
                LogManager.RegisterLogMessage(LogManager.LogType.Error, LogMessages.DependencyNotFound(
                    GetType().ToString(), physicsEngine.GetType().ToString()));
                return;
            }
            
            physicsEngine.ForceProviders.Add(this);
        }

        public void Configure(ForceProviderConfig forceProviderConfig)
        {
            _forceProviderConfig = forceProviderConfig;
        }
        
        protected void OnDestroy()
        {
            if (Context.TryGetComponentFromContainer(out PhysicsEngine physicsEngine))
            {
                physicsEngine.ForceProviders.Remove(this);
            }
        }
    }
}