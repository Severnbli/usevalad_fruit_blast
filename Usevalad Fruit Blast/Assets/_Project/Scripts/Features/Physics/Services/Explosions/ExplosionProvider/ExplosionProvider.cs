using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Figures;
using _Project.Scripts.Features.Physics.Forces.ExplosionForceProvider;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionFinder;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Explosions.ExplosionProvider
{
    public class ExplosionProvider : BaseFeature, IConfigurableFeature<ExplosionProviderConfig>
    {
        private ObjectsContainer _objectsContainer;
        private ExplosionForceProvider _explosionForceProvider;
        private ExplosionProviderConfig _explosionProviderConfig;

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _explosionForceProvider);
            Context.TryGetComponentFromContainer(out _objectsContainer);
        }

        public void Configure(ExplosionProviderConfig explosionProviderConfig)
        {
            _explosionProviderConfig = explosionProviderConfig;
        }
        
        public void Explode(ExplodingObject.ExplodingObject explodingObject)
        {
            if (!explodingObject.TryGetComponent(out BaseCollider collider))
            {
                return;
            }
            
            collider.GetFigure().GetBoundingCircle(out var point, out var radius);
            var boundCircle = new CircleFigure(point, radius + _explosionProviderConfig.MaxAffectedDistance);

            foreach (var containerableObject in _objectsContainer.ContainerableObjects)
            {
                if (!containerableObject.TryGetComponent(out BaseCollider containingCollider))
                {
                    continue;
                }
                
                var figure = containingCollider.GetFigure();
                
                if (!CollisionFinder.IsFiguresCollide(boundCircle, figure))
                {
                    continue;
                }

                figure.GetBoundingCircle(out var containingPoint, out var containingRadius);

                var distanceVector = containingPoint - point;
                
                var distance = distanceVector.magnitude - radius - containingRadius;
                var t = Mathf.Clamp01(distance / _explosionProviderConfig.MaxAffectedDistance);
                var falloff = 1f - t;
                
                var affectedVector = distanceVector.normalized * falloff;
                
                _explosionForceProvider.TryAddExplosionData(containerableObject.DynamicBody, affectedVector);
            }
        }
    }
}