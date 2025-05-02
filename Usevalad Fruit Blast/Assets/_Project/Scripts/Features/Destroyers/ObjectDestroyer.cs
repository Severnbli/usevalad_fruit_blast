using System.Collections.Generic;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Destroyers.Config;
using _Project.Scripts.Features.Destroyers.Services.InfectiousProvider;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers
{
    public abstract class ObjectDestroyer : BaseFeature
    {
        [SerializeField] private InfectiousProvider _infectiousProvider;
        
        [SerializeField] protected float _destroyDelay;
        
        public InfectiousProvider InfectiousProvider => _infectiousProvider;
        public float DestroyDelay => _destroyDelay;
        
        public List<DestroyableObject.DestroyableObject> DestroyableObjects = new();

        public override void Init(IFeatureConfig config)
        {
            if (config is not BaseObjectDestroyerConfig baseObjectDestroyerConfig)
            {
                return;
            }
            
            _destroyDelay = baseObjectDestroyerConfig.DestroyDelay;

            if (!Context.Container.TryGetComponent(out _infectiousProvider))
            {
                Debug.LogError("Check system priority setup: InfectiousProvider must be earlier than physics ObjectDestroyer!");
            }
        }

        protected void DestroyObject(DestroyableObject.DestroyableObject objectToDestroy, float destroyDelay)
        {
            switch (objectToDestroy.DestroyableType)
            {
                case DestroyableObject.DestroyableObject.Type.Default:
                {
                    DefaultDestroy(objectToDestroy, _destroyDelay);
                    break;
                }
                case DestroyableObject.DestroyableObject.Type.Infectious:
                {
                    InfectiousDestroy(objectToDestroy, _destroyDelay, new());
                    break;
                }
                default:
                {
                    return;
                }
            }
        }

        private void DefaultDestroy(DestroyableObject.DestroyableObject objectToDestroy, float destroyDelay)
        {
            Destroy(objectToDestroy.gameObject, destroyDelay);
        }

        private void InfectiousDestroy(DestroyableObject.DestroyableObject objectToDestroy, float destroyDelay, 
            HashSet<DestroyableObject.DestroyableObject> objectsToDestroy)
        {
            DefaultDestroy(objectToDestroy, destroyDelay);

            foreach (var destroyableObject in DestroyableObjects)
            {
                if (destroyableObject.Id == objectToDestroy.Id
                    && Vector2.Distance(destroyableObject.transform.position, objectToDestroy.transform.position) 
                    <= _infectiousProvider.InfectiousDistance
                    && !objectsToDestroy.Contains(destroyableObject))
                {
                    objectsToDestroy.Add(destroyableObject);
                    var delay = destroyDelay + _destroyDelay;
                    
                    if (destroyableObject.DestroyableType == DestroyableObject.DestroyableObject.Type.Infectious)
                    {
                        InfectiousDestroy(destroyableObject, delay, objectsToDestroy);
                    }
                    else
                    {
                        DefaultDestroy(destroyableObject, delay);
                    }
                }
            }
        }
    }
}