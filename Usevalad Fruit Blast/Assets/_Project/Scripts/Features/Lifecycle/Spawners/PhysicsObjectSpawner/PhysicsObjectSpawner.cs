using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Dynamic;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner
{
    public abstract class PhysicsObjectSpawner : ObjectSpawner, IConfigurableFeature<PhysicsObjectSpawnerConfig>
    {
        [SerializeField] protected PhysicsObjectSpawnerConfig _physicsObjectSpawnerConfig;
        
        public PhysicsObjectSpawnerConfig PhysicsObjectSpawnerConfig => _physicsObjectSpawnerConfig;

        public void Configure(PhysicsObjectSpawnerConfig physicsObjectSpawnerConfig)
        {
            base.Configure(physicsObjectSpawnerConfig.ObjectSpawnerConfig);
            
            _physicsObjectSpawnerConfig = physicsObjectSpawnerConfig;
        }
        
        public override bool TryGetConfiguredObject(out GameObject configuredObject)
        {
            if (!base.TryGetConfiguredObject(out configuredObject))
            {
                return false;
            }
            
            ConfigureWithPhysicsSettings(configuredObject);
            
            return true;
        }

        private void ConfigureWithPhysicsSettings(GameObject configuredObject)
        {
            if (!configuredObject.TryGetComponent(out DynamicBody dynamicBody))
            {
                Debug.LogWarning($"Physics Object Spawner: {configuredObject.name} is missing a DynamicBody");
                return;
            }
            
            var randMass = (float) _randomProvider.Random.NextDouble() 
                           * (_physicsObjectSpawnerConfig.MaxMass - _physicsObjectSpawnerConfig.MinMass) 
                           + _physicsObjectSpawnerConfig.MinMass;
            
            var randBounciness = (float) _randomProvider.Random.NextDouble() 
                                 * (_physicsObjectSpawnerConfig.MaxBounciness - _physicsObjectSpawnerConfig.MinBounciness)
                                 + _physicsObjectSpawnerConfig.MinBounciness;
            
            var randGravityFactor = (float) _randomProvider.Random.NextDouble() 
                                    * (_physicsObjectSpawnerConfig.MaxGravityFactor - _physicsObjectSpawnerConfig.MinGravityFactor)
                                    + _physicsObjectSpawnerConfig.MinGravityFactor;
            
            var randStartVelocity = new Vector2(
                (float) _randomProvider.Random.NextDouble() 
                * (_physicsObjectSpawnerConfig.MaxStartVelocity.x - _physicsObjectSpawnerConfig.MinStartVelocity.x) 
                + _physicsObjectSpawnerConfig.MinStartVelocity.x,
                (float) _randomProvider.Random.NextDouble()
                * (_physicsObjectSpawnerConfig.MaxStartVelocity.y - _physicsObjectSpawnerConfig.MinStartVelocity.y) 
                + _physicsObjectSpawnerConfig.MinStartVelocity.y
            );
            
            dynamicBody.Mass = randMass;
            dynamicBody.BouncinessFactor = randBounciness;
            dynamicBody.GravityFactor = randGravityFactor;
            dynamicBody.Velocity = randStartVelocity;
        }
    }
}