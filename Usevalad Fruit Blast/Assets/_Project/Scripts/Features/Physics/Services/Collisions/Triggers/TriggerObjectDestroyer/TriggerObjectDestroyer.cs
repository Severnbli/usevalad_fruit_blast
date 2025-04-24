using System;
using System.Linq;
using _Project.Scripts.Features.Physics.Colliders;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.Triggers.TriggerObjectDestroyer
{
    public class TriggerObjectDestroyer : ColliderTrigger
    {
        [SerializeField] private float _timeToDestroy;
        [SerializeField] private DestroyMethod _destroyMethod;
        [SerializeField] private string[] _targetTags;

        [Serializable]
        public enum DestroyMethod
        {
            Everything = 0,
            ByTag = 1
        }
        
        public override void OnColliderTriggerEnter(BaseCollider collider)
        {
            DestroyByMethod(collider.gameObject);
        }

        private void DestroyByMethod(GameObject objectToDestroy)
        {
            var isDestroyNeeded = false;
            
            switch (_destroyMethod)
            {
                case DestroyMethod.Everything:
                {
                    isDestroyNeeded = true;
                    break;
                }
                case DestroyMethod.ByTag:
                {
                    if (_targetTags.Contains(objectToDestroy.tag))
                    {
                        isDestroyNeeded = true;
                    }

                    break;
                }
            }

            if (isDestroyNeeded)
            {
                Destroy(objectToDestroy, _timeToDestroy);
            }
        }
    }
}