using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Controls.Pointer.Mouse.MouseProvider;
using _Project.Scripts.Features.Controls.Pointer.Touch.TouchProvider;
using _Project.Scripts.Features.Destroyers.ClickObjectDestroyer.Config;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.ClickObjectDestroyer
{
    public class ClickObjectDestroyer : ObjectDestroyer
    {
        [SerializeField] private TouchProvider _touchProvider;
        [SerializeField] private MouseProvider _mouseProvider;
        [SerializeField] private float _destroyDistance;
        
        public TouchProvider TouchProvider => _touchProvider;
        public MouseProvider MouseProvider => _mouseProvider;

        private void DestroyObjectAt(Vector2 position)
        {
            if (!TryGetNearestDestroyableObject(position, out var destroyableObject))
            {
                return;
            }
            
            destroyableObject.DestroyDestroyableObject(this);
        }

        public bool TryGetNearestDestroyableObject(Vector2 position, out DestroyableObject.DestroyableObject nearestDestroyableObject)
        {
            nearestDestroyableObject = null;
            
            if (DestroyableObjects.Count == 0)
            {
                return false;
            }
            
            var lastMinimumDistance = float.MaxValue;
            var isNearestFound = false;

            foreach (var destroyableObject in DestroyableObjects)
            {
                var distance = Vector2.Distance(position, destroyableObject.transform.position);

                if (distance < lastMinimumDistance && distance <= _destroyDistance)
                {
                    isNearestFound = true;
                    nearestDestroyableObject = destroyableObject;
                    lastMinimumDistance = distance;
                }
            }
            
            return isNearestFound;
        }

        public override void Init(IFeatureConfig config)
        {
            base.Init(config);

            if (config is not ClickObjectDestroyerConfig clickObjectDestroyerConfig)
            {
                return;
            }
            
            _destroyDistance = clickObjectDestroyerConfig.DestroyDistance;

            if (Context.Container == null)
            {
                return;
            }

            if (Context.Container.TryGetComponent<TouchProvider>(out var touchProvider))
            {
                _touchProvider = touchProvider;
                
                _touchProvider.OnBeginTouch += DestroyObjectAt;
            }
            else
            {
                Debug.LogError("Check system priority setup: touch provider must be earlier than touch provider!");
            }
            
            if (Context.Container.TryGetComponent<MouseProvider>(out var mouseProvider))
            {
                _mouseProvider = mouseProvider;
                
                _mouseProvider.OnPrimaryMouseButtonDown += DestroyObjectAt;
            }
            else
            {
                Debug.LogError("Check system priority setup: mouse provider must be earlier than touch provider!");
            }
        }

        private void OnDestroy()
        {
            if (_touchProvider != null)
            {
                _touchProvider.OnBeginTouch -= DestroyObjectAt;
            }

            if (_mouseProvider != null)
            {
                _mouseProvider.OnPrimaryMouseButtonDown -= DestroyObjectAt;
            }
        }
    }
}