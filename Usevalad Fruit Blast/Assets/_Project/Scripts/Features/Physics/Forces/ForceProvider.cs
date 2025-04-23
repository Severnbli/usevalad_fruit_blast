using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Forces
{
    public abstract class ForceProvider : BaseFeature
    {
        [SerializeField] private float _factor;
        [SerializeField] private Vector2 _direction;
        
        public float Factor { get => _factor; set => _factor = value; }
        public Vector2 Direction { get => _direction; set => _direction = value; }

        public Vector2 GetForce()
        {
            return _factor * _direction;
        }

        public override void Init(IFeatureConfig config)
        {
            if (config is not ForceProviderConfig forceProviderConfig)
            {
                return;
            }
            
            _factor = forceProviderConfig.Factor;
            _direction = forceProviderConfig.Direction;
            
            var physicsEngine = Context.Container.GetComponent<PhysicsEngine>();

            if (physicsEngine == null)
            {
                Debug.LogError("Check system priority setup: physics engine must be earlier than force provider!");
            }
            
            physicsEngine.ForceProviders.Add(this);
        }
        
        protected void OnDestroy()
        {
            if (Context.Container != null && Context.Container.TryGetComponent(out PhysicsEngine engine))
            {
                engine.ForceProviders.Remove(this);
            }
        }
    }
}