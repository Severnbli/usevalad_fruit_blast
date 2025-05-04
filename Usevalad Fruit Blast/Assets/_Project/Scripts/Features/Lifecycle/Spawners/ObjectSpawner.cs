using System.Linq;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Lifecycle.Objects;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Random;
using _Project.Scripts.System;
using _Project.Scripts.System.Logs;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Spawners
{
    public abstract class ObjectSpawner : BaseFeature, IConfigurableFeature<ObjectSpawnerConfig>
    {
        [SerializeField] protected ObjectSpawnerConfig _objectSpawnerConfig;
        
        protected RandomProvider _randomProvider;
        protected ObjectsContainer _objectsContainer;
        
        public ObjectSpawnerConfig ObjectSpawnerConfig => _objectSpawnerConfig;
        public RandomProvider RandomProvider => _randomProvider;
        public ObjectsContainer ObjectsContainer => _objectsContainer;
        
        public abstract void Spawn();
        
        public override void Init()
        {
            if (!Context.TryGetComponentFromContainer(out _randomProvider))
            {
                Debug.LogError(LogMessages.DependencyNotFound(GetType().ToString(), 
                    _randomProvider.GetType().ToString()));
            }
            
            if (!Context.TryGetComponentFromContainer(out _objectsContainer))
            {
                Debug.LogError(LogMessages.DependencyNotFound(GetType().ToString(), 
                    _objectsContainer.GetType().ToString()));
            }
        }

        public void Configure(ObjectSpawnerConfig objectSpawnerConfig)
        {
            _objectSpawnerConfig = objectSpawnerConfig;
        }

        public virtual bool TryGetConfiguredObject(out GameObject configuredObject)
        {
            configuredObject = Instantiate(_objectSpawnerConfig.Prefab, Vector3.zero, Quaternion.identity);

            if (configuredObject == null)
            {
                return false;
            }
            
            var randScale = (float) _randomProvider.Random.NextDouble() 
                            * (_objectSpawnerConfig.MaxScale - _objectSpawnerConfig.MinScale) 
                            + _objectSpawnerConfig.MinScale;
            
            configuredObject.transform.localScale = new Vector3(randScale, randScale);
            configuredObject.transform.SetParent(_objectsContainer.GetObjectContainerTransform());
           
            ConfigureWithActiveGroups(configuredObject);
            
            return true;
        }

        private void ConfigureWithActiveGroups(GameObject configuredObject)
        {
            var activeSpawnGroups = _objectSpawnerConfig.SpawnGroups.ToList();
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
            
            if (configuredObject.TryGetComponent<ContainerableObject>(out var containerableObject))
            { 
                containerableObject.Id = randGroup.Id;
            }
        }
    }
}