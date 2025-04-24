using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Spawners.PhysicsObjectSpawner.Config;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners.PhysicsObjectSpawner
{
    public abstract class PhysicsObjectSpawner : ObjectSpawner
    {
        [SerializeField] protected PhysicsGroupObjectConfig[] _physicsGroupsObjectConfigs;
        
        public PhysicsGroupObjectConfig[] PhysicsGroupsObjectConfigs => _physicsGroupsObjectConfigs;
        
        public virtual bool TryGetConfiguredObject(out GameObject resultObject)
        {
            var randGroup = _physicsGroupsObjectConfigs[_randomProvider.Random.Next(_physicsGroupsObjectConfigs.Length)];

            if (randGroup == null)
            {
                resultObject = null;
                return false;
            }
            
            var randObjectConfig = randGroup.ObjectConfig[_randomProvider.Random.Next(randGroup.ObjectConfig.Length)];

            if (randObjectConfig == null)
            {
                resultObject = null;
                return false;
            }
            
            var randScale = (float) _randomProvider.Random.NextDouble() 
                * (randGroup.MaxScale - randGroup.MinScale) + randGroup.MinScale;
            
            resultObject = Instantiate(randObjectConfig.Prefab, Vector3.zero, Quaternion.identity);
            resultObject.transform.SetParent(transform);
            resultObject.transform.localScale = new Vector3(randScale, randScale);

            if (!resultObject.TryGetComponent(out DynamicBody dynamicBody))
            {
                Debug.LogWarning($"Physics Object spawner: {resultObject.name} is missing a DynamicBody");
                return true;
            }
            
            var randMass = (float) _randomProvider.Random.NextDouble() 
                * (randGroup.MaxMass - randGroup.MinMass) + randGroup.MinMass;
            var randSpeed = (float) _randomProvider.Random.NextDouble()
                * (randGroup.MaxStartSpeed - randGroup.MinStartSpeed) + randGroup.MinStartSpeed;
            
            dynamicBody.Mass = randMass;
            dynamicBody.Velocity = randSpeed * randGroup.StartVector;
            
            return true;
        }
    }
}