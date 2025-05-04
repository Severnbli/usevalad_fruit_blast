using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Controls.Pointer;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Lifecycle.Objects;
using _Project.Scripts.Features.Lifecycle.Spawners;
using _Project.Scripts.System;
using _Project.Scripts.System.Logs.Logger;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.LifecycleProvider
{
    public class LifecycleProvider : BaseFeature
    {
        [SerializeField] private ObjectSpawner _objectSpawner;
        [SerializeField] private PointerProvider[] _pointerProviders;
        [SerializeField] private FieldCatcher _fieldCatcher;
        [SerializeField] private ObjectsContainer _objectsContainer;
        
        public ObjectSpawner ObjectSpawner => _objectSpawner;
        public PointerProvider[] PointerProviders => _pointerProviders;
        public FieldCatcher FieldCatcher => _fieldCatcher;
        public ObjectsContainer ObjectsContainer => _objectsContainer;
        
        public override void Init()
        {
            base.Init();

            if (!Context.TryGetComponentFromContainer(out _objectSpawner))
            {
                LogManager.RegisterLogMessage(LogManager.LogType.Error, LogMessages.DependencyNotFound(
                    GetType().ToString(), _objectSpawner.GetType().ToString()));
            }
            
            if (!Context.TryGetComponentsFromContainer(out _pointerProviders))
            {
                LogManager.RegisterLogMessage(LogManager.LogType.Error, LogMessages.DependencyNotFound(
                    GetType().ToString(), _pointerProviders.GetType().ToString()));
            }
            
            if (!Context.TryGetComponentFromContainer(out _fieldCatcher))
            {
                LogManager.RegisterLogMessage(LogManager.LogType.Error, LogMessages.DependencyNotFound(
                    GetType().ToString(), _fieldCatcher.GetType().ToString()));
            }
            
            if (!Context.TryGetComponentFromContainer(out _objectsContainer))
            {
                LogManager.RegisterLogMessage(LogManager.LogType.Error, LogMessages.DependencyNotFound(
                    GetType().ToString(), _objectsContainer.GetType().ToString()));
            }
        }

        private void SetProvidersAvailability(bool isEnable)
        {
            foreach (var pointerProvider in _pointerProviders)
            {
                pointerProvider?.SetIsEnable(isEnable);
            }
        }

        
    }
}