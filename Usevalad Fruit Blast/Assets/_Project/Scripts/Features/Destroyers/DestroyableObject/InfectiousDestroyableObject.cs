using System.Collections.Generic;
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

        public void DestroyInfectious(ObjectDestroyer destroyer)
        {
            foreach (var destroyableObject in destroyer.DestroyableObjects)
            {
                if (destroyableObject is not InfectiousDestroyableObject infectiousDestroyableObject
                    || infectiousDestroyableObject._infectiousGroupId != _infectiousGroupId)
                {
                    continue;
                }
                
                var distance = Vector2.Distance(destroyableObject.transform.position, transform.position);

                if (distance <= _infectiousRadius)
                {
                    destroyableObject.DestroyDestroyableObject(destroyer);
                }
            }
        }

        public override void DestroyDestroyableObject(ObjectDestroyer destroyer)
        {
            base.DestroyDestroyableObject(destroyer);
            
            DestroyInfectious(destroyer);
        }
    }
}