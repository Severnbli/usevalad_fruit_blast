using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Lifecycle.Objects;

using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Explosions.ExplodingObject
{
    public class ExplodingObject : MonoBehaviour
    {
        private ExplosionProvider.ExplosionProvider _explosionProvider;
        public ContainerableObject ContainerableObject { get; private set; }

        private void Start()
        {
            ContainerableObject = GetComponent<ContainerableObject>();
            
            if (!ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                return;
            }

            var context = systemCoordinator.Context;

            context.TryGetComponentFromContainer(out _explosionProvider);
        }

        private void OnDestroy()
        {
            _explosionProvider?.Explode(this);
        }
    }
}