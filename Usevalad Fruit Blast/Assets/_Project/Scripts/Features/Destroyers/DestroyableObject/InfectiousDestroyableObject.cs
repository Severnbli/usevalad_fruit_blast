using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.DestroyableObject
{
    public class InfectiousDestroyableObject : DestroyableObject
    {
        [SerializeField] private int _infectiousGroupId;
        [SerializeField] private float _infectiousRadius;
        
        public int InfectiousGroupId => _infectiousGroupId;
        public float InfectiousRadius => _infectiousRadius;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            DestroySameInfectiousGroupByInfectiousRadius();
        }

        private void DestroySameInfectiousGroupByInfectiousRadius()
        {
            if (!Context.TryGetComponentsFromContainer<ObjectDestroyer>(out var destroyers))
            {
                return;
            }

            foreach (var destroyer in destroyers)
            {
                foreach (var destroyableObject in destroyer.DestroyableObjects)
                {
                    if (destroyableObject is not InfectiousDestroyableObject infectiousDestroyableObject
                        || infectiousDestroyableObject.InfectiousGroupId != _infectiousGroupId)
                    {
                        continue;
                    }
                    
                    var distance = Vector2.Distance(destroyableObject.transform.position, transform.position);

                    if (distance <= _infectiousRadius)
                    {
                        Destroy(destroyableObject.gameObject);
                    }
                }
            }
        }
    }
}