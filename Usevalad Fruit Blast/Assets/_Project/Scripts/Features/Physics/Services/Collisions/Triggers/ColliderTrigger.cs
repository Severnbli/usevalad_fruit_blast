using _Project.Scripts.Features.Physics.Colliders;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.Triggers
{
    public abstract class ColliderTrigger : MonoBehaviour
    {
        public abstract void OnColliderTriggerEnter(BaseCollider other);
    }
}