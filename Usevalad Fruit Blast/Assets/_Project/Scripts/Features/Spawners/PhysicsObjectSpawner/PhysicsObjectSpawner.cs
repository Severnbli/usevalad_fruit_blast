using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Spawners.PhysicsObjectSpawner.Config;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners.PhysicsObjectSpawner
{
    public abstract class PhysicsObjectSpawner : ObjectSpawner
    {
        [SerializeField] protected PhysicsSpawnerConfig _physicsSpawnerConfig;
        
        public PhysicsSpawnerConfig PhysicsSpawnerConfig => _physicsSpawnerConfig;

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
                           * (_physicsSpawnerConfig.MaxMass - _physicsSpawnerConfig.MinMass) 
                           + _physicsSpawnerConfig.MinMass;
            
            var randBounciness = (float) _randomProvider.Random.NextDouble() 
                                 * (_physicsSpawnerConfig.MaxBounciness - _physicsSpawnerConfig.MinBounciness)
                                 + _physicsSpawnerConfig.MinBounciness;
            
            var randGravityFactor = (float) _randomProvider.Random.NextDouble() 
                                    * (_physicsSpawnerConfig.MaxGravityFactor - _physicsSpawnerConfig.MinGravityFactor)
                                    + _physicsSpawnerConfig.MinGravityFactor;
            
            var randStartVelocity = new Vector2(
                (float) _randomProvider.Random.NextDouble() 
                * (_physicsSpawnerConfig.MaxStartVelocity.x - _physicsSpawnerConfig.MinStartVelocity.x) 
                + _physicsSpawnerConfig.MinStartVelocity.x,
                (float) _randomProvider.Random.NextDouble()
                * (_physicsSpawnerConfig.MaxStartVelocity.y - _physicsSpawnerConfig.MinStartVelocity.y) 
                + _physicsSpawnerConfig.MinStartVelocity.y
            );
            
            dynamicBody.Mass = randMass;
            dynamicBody.BouncinessFactor = randBounciness;
            dynamicBody.GravityFactor = randGravityFactor;
            dynamicBody.Velocity = randStartVelocity;
        }
    }
}