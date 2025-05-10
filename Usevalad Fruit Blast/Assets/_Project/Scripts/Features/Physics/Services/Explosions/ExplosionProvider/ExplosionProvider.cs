using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Physics.Colliders;
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
            if (!explodingObject.TryGetComponent(out BaseCollider explodingCollider))
            {
                return;
            }

            var explodingCircleFigure = explodingCollider.GetBoundingCircleFigure();
            explodingCircleFigure.Radius += _explosionProviderConfig.MaxAffectedDistance;
            
            foreach (var containerableObject in _objectsContainer.ContainerableObjects)
            {
                if (!containerableObject.TryGetComponent(out BaseCollider collider))
                {
                    continue;
                }
                
                var circleFigure = collider.GetBoundingCircleFigure();
                
                if (!CollisionFinder.IsCircleCircleCollide(explodingCircleFigure, circleFigure))
                {
                    continue;
                }

                var distanceVector = circleFigure.Point - explodingCircleFigure.Point;
                
                var distance = distanceVector.magnitude - explodingCircleFigure.Radius - circleFigure.Radius;
                var t = Mathf.Clamp01(distance / _explosionProviderConfig.MaxAffectedDistance);
                var falloff = 1f - t;
                
                var affectedVector = distanceVector.normalized * falloff;
                
                _explosionForceProvider.TryAddExplosionData(containerableObject.DynamicBody, affectedVector);
            }
        }
    }
}