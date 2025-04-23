using _Project.Scripts.Features.Physics.Colliders;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.Triggers.TriggerObjectDestroyer
{
    public class TriggerObjectDestroyer : ColliderTrigger
    {
        [SerializeField] private float _timeToDestroy;

        public override void OnColliderTriggerEnter(BaseCollider collider)
        {
            Destroy(collider.gameObject, _timeToDestroy);
        }
    }
}