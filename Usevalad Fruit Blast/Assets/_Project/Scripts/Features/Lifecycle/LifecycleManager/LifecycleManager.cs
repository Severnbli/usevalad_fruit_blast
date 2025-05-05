using System.Threading;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Controls.Pointer;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Lifecycle.Spawners;
using _Project.Scripts.System;
using _Project.Scripts.System.Logs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.LifecycleManager
{
    public class LifecycleManager : BaseFeature, IConfigurableFeature<LifecycleManagerConfig>
    {
        [SerializeField] private LifecycleManagerConfig _lifecycleManagerConfig;
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
                Debug.LogError(LogMessages.DependencyNotFound(GetType().ToString(), 
                    _objectSpawner.GetType().ToString()));
            }
            
            if (!Context.TryGetComponentsFromContainer(out _pointerProviders))
            {
                Debug.LogError(LogMessages.DependencyNotFound(GetType().ToString(), 
                    _pointerProviders.GetType().ToString()));
            }
            
            if (!Context.TryGetComponentFromContainer(out _fieldCatcher))
            {
                Debug.LogError(LogMessages.DependencyNotFound(GetType().ToString(), 
                    _fieldCatcher.GetType().ToString()));
            }
            
            if (!Context.TryGetComponentFromContainer(out _objectsContainer))
            {
                Debug.LogError(LogMessages.DependencyNotFound(GetType().ToString(), 
                    _objectsContainer.GetType().ToString()));
            }
        }

        public void Configure(LifecycleManagerConfig lifecycleManagerConfig)
        {
            _lifecycleManagerConfig = lifecycleManagerConfig;
            RunLifecycle();
        }

        public void RunLifecycle()
        {
            // _isFillingActive = true;
            // SetPointerProvidersAvailability(false);
            FillTheCatcher(this.GetCancellationTokenOnDestroy()).Forget();
        }
        
        private async UniTask FillTheCatcher(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var corruptedArea = ObjectsContainer.GetTotalArea() / FieldCatcher.GetArea();
                
                if (corruptedArea < _lifecycleManagerConfig.MaxCorruptedFieldCatcherArea)
                {
                    ObjectSpawner.Spawn();
                }
        
                await UniTask.WaitForSeconds(0.2f, cancellationToken: token);
            }
        }

        private void SetPointerProvidersAvailability(bool isEnable)
        {
            foreach (var pointerProvider in _pointerProviders)
            {
                pointerProvider?.SetIsEnable(isEnable);
            }
        }
    }
}