using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Features.Controls.Pointer.MouseProvider;
using _Project.Scripts.Features.Controls.Pointer.Touch;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Lifecycle.Objects;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Figures;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionFinder;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Destroyers.ClickObjectDestroyer
{
    public class ClickObjectDestroyer : ObjectDestroyer, IConfigurableFeature<ClickObjectDestroyerConfig>,
        IDestroyableFeature
    {
        private TouchProvider _touchProvider;
        private MouseProvider _mouseProvider;
        private ObjectsContainer _objectsContainer;
        private FieldCatcher _fieldCatcher;
        private ClickObjectDestroyerConfig _clickObjectDestroyerConfig;

        private readonly Dictionary<ContainerableObject, int> _objectsToDestroy = new();
        private readonly Dictionary<ContainerableObject, int> _filterObjects = new();

        public override void Init()
        {
            base.Init();

            if (Context.TryGetComponentFromContainer(out _touchProvider))
            {
                _touchProvider.OnBeginTouch += DestroyObjectAt;
            }

            if (Context.TryGetComponentFromContainer(out _mouseProvider))
            {
                _mouseProvider.OnPrimaryMouseButtonDown += DestroyObjectAt;
            }

            Context.TryGetComponentFromContainer(out _objectsContainer);

            Context.TryGetComponentFromContainer(out _fieldCatcher);
        }

        public void Configure(ClickObjectDestroyerConfig clickObjectDestroyerConfig)
        {
            _clickObjectDestroyerConfig = clickObjectDestroyerConfig;
        }

        public void OnDestroy()
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
        
        public override void DestroyObjectAt(Vector2 position)
        {
            if (!TryGetNearestObjectThatMatchRules(position, out var nearestObject)
                || _objectsToDestroy.ContainsKey(nearestObject))
            {
                return;
            }
            
            _objectsToDestroy.Clear();
            _objectsToDestroy.Add(nearestObject, 0);
            
            SearchForInfectedObjects(nearestObject, _objectsToDestroy);

            if (_objectsToDestroy.Count < _clickObjectDestroyerConfig.MinInfectedObjects 
                && TryGetBetterInfectedGroup(_objectsToDestroy, out var betterInfectedObjects))
            {
                return;
            }
            
            DestroyWithEasing(_objectsToDestroy);
        }

        public override bool TryGetNearestObjectThatMatchRules(Vector2 position, out ContainerableObject nearestObject)
        {
            nearestObject = null;

            if (!IsClickMatchFieldCatcherRules(position))
            {
                return false;
            }
            
            var minDistance = Mathf.Infinity;
            
            foreach (var containerableObject in _objectsContainer.ContainerableObjects)
            {
                if (!containerableObject.TryGetComponent(out BaseCollider collider))
                {
                    continue;
                }
                
                var figure = collider.GetFigure();
                figure.GetBoundingCircle(out var point, out var radius);
                
                var distance = Vector2.Distance(position, point) - radius;

                if (distance <= _clickObjectDestroyerConfig.ClickOffset && distance < minDistance)
                {
                    nearestObject = containerableObject;
                    minDistance = distance;
                }
            }

            return nearestObject != null;
        }

        private bool IsClickMatchFieldCatcherRules(Vector2 position)
        {
            var topMarginAxis = _fieldCatcher.GetPosition().y + _fieldCatcher.GetFieldProvider().GetFieldSize().y / 2f
                                - _fieldCatcher.FieldCatcherConfig.Margin.Top;

            return position.y <= topMarginAxis;
        }

        private void SearchForInfectedObjects(ContainerableObject currentObject, 
            Dictionary<ContainerableObject, int> infectedObjects, Dictionary<ContainerableObject, int> filteredObjects = null)
        {
            if (ReferenceEquals(infectedObjects, filteredObjects))
            {
                Debug.LogError("ClickObjectDestroyer: filter and infected objects are the same!");
                return;
            }
            
            if (!currentObject.TryGetComponent(out BaseCollider currentCollider))
            {
                return;
            }
            
            currentCollider.GetFigure().GetBoundingCircle(out var currentPoint, out var currentRadius);
            var infectiousFigure = new CircleFigure(currentPoint, currentRadius);
            infectiousFigure.Radius += _clickObjectDestroyerConfig.InfectionDistance;
            
            var newInfectedObjects = new List<ContainerableObject>();

            var highestRemovalOrder = infectedObjects.Values.Max();
            
            foreach (var containerableObject in _objectsContainer.ContainerableObjects)
            {
                if ((filteredObjects?.ContainsKey(containerableObject) ?? false)
                    || infectedObjects.ContainsKey(containerableObject)
                    || currentObject.Id != containerableObject.Id
                    || !containerableObject.TryGetComponent(out BaseCollider collider))
                {
                    continue;
                }
                
                collider.GetFigure().GetBoundingCircle(out var point, out var radius);
                var targetFigure = new CircleFigure(point, radius);

                if (!CollisionFinder.IsCircleCircleCollide(infectiousFigure, targetFigure))
                {
                    continue;
                }
                
                filteredObjects?.Add(containerableObject, highestRemovalOrder + 1);
                infectedObjects.Add(containerableObject, highestRemovalOrder + 1);
                newInfectedObjects.Add(containerableObject);
            }

            foreach (var newInfectedObject in newInfectedObjects)
            {
                SearchForInfectedObjects(newInfectedObject, infectedObjects, filteredObjects);
            }
        }

        private bool TryGetBetterInfectedGroup(Dictionary<ContainerableObject, int> selectedInfectedObjects,
            out Dictionary<ContainerableObject, int> betterInfectedObjects)
        {
            _filterObjects.Clear();
            _filterObjects.AddRange(selectedInfectedObjects);
            var listOfResultGroup = new List<Dictionary<ContainerableObject, int>>();
            
            foreach (var containerableObject in _objectsContainer.ContainerableObjects)
            {
                if (_filterObjects.ContainsKey(containerableObject))
                {
                    continue;
                }

                var group = new Dictionary<ContainerableObject, int>();
                group.Add(containerableObject, 0);
                _filterObjects.Add(containerableObject, 0);
                
                SearchForInfectedObjects(containerableObject, group, _filterObjects);
                listOfResultGroup.Add(group);
            }

            var betterCount = 0;
            betterInfectedObjects = null;
            
            foreach (var group in listOfResultGroup)
            {
                if (group.Count > betterCount)
                {
                    betterCount = group.Count;
                    betterInfectedObjects = group;
                }
            }

            if (betterInfectedObjects == null)
            {
                return false;
            }
            
            return betterInfectedObjects.Count > selectedInfectedObjects.Count;
        }
        
        public void DestroyWithEasing(Dictionary<ContainerableObject, int> infectedObjects)
        {
            if (infectedObjects.Count == 0)
            {
                return;
            }
            
            var minOrder = infectedObjects.Values.Min();
            var maxOrder = infectedObjects.Values.Max();
            var orderRange = maxOrder - minOrder;

            Dictionary<int, float> orderToDelay = new();

            foreach (var order in infectedObjects.Values.Distinct())
            {
                var t = orderRange == 0 ? 0f : (order - minOrder) / (float)orderRange;
                var delay = _clickObjectDestroyerConfig.DestroyCurve.Evaluate(t) * _clickObjectDestroyerConfig.DestroyDuration;
                orderToDelay[order] = delay;
            }
            
            foreach (var (obj, order) in infectedObjects)
            {
                if (orderToDelay.TryGetValue(order, out var delay))
                {
                    DestroyObject(obj.gameObject, delay).Forget();
                }
            }
        }
    }
}