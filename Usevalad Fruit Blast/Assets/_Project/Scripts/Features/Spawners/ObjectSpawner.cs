using System.Linq;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Destroyers.DestroyableObject;
using _Project.Scripts.Features.Random;
using _Project.Scripts.Features.Spawners.Config;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners
{
    public abstract class ObjectSpawner : BaseFeature
    {
        [SerializeField] protected BaseSpawnerConfig _baseSpawnerConfig;
        protected RandomProvider _randomProvider;
        
        public BaseSpawnerConfig BaseSpawnerConfig => _baseSpawnerConfig;
        public RandomProvider RandomProvider => _randomProvider;
        
        public abstract void Spawn();
        
        public override void Init(IFeatureConfig config)
        {
            _randomProvider = Context.Container.GetComponent<RandomProvider>();

            if (_randomProvider == null)
            {
                Debug.LogError("Check system priority setup: random provider must be earlier than object spawner!");
            }
        }

        public virtual bool TryGetConfiguredObject(out GameObject configuredObject)
        {
            configuredObject = Instantiate(_baseSpawnerConfig.Prefab, Vector3.zero, Quaternion.identity);

            if (configuredObject == null)
            {
                return false;
            }
            
            var randScale = (float) _randomProvider.Random.NextDouble() 
                            * (_baseSpawnerConfig.MaxScale - _baseSpawnerConfig.MinScale) 
                            + _baseSpawnerConfig.MinScale;
            
            configuredObject.transform.localScale = new Vector3(randScale, randScale);
            configuredObject.transform.SetParent(transform);
           
            ConfigureWithActiveGroups(configuredObject);
            
            return true;
        }

        private void ConfigureWithActiveGroups(GameObject configuredObject)
        {
            var activeSpawnGroups = _baseSpawnerConfig.SpawnGroups.ToList();
            activeSpawnGroups.RemoveAll(x => !x.IsActive);
            
            if (activeSpawnGroups.Count == 0)
            {
                return;
            }
            
            var randGroup = activeSpawnGroups[_randomProvider.Random.Next(activeSpawnGroups.Count)];
            
            if (!configuredObject.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                Debug.LogWarning($"Object Spawner: {configuredObject.name} is missing a SpriteRenderer");
            }
            else
            {
                spriteRenderer.sprite = randGroup.Sprite;
            }
            
            if (configuredObject.TryGetComponent<DestroyableObject>(out var destroyableObject))
            { 
                destroyableObject.Id = randGroup.Id;
            }
        }
    }
}