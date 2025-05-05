using System.Threading;
using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
using _Project.Scripts.Features.Controls.Pointer;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Lifecycle.Spawners;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.LifecycleManager
{
    public class LifecycleManager : BaseFeature, IConfigurableFeature<LifecycleManagerConfig>
    {
        private LifecycleManagerConfig _lifecycleManagerConfig;
        private ObjectSpawner _objectSpawner;
        private PointerProvider[] _pointerProviders;
        private FieldCatcher _fieldCatcher;
        private ObjectsContainer _objectsContainer;
        private SystemCoordinator _systemCoordinator;
        
        public ObjectSpawner ObjectSpawner => _objectSpawner;
        public PointerProvider[] PointerProviders => _pointerProviders;
        public FieldCatcher FieldCatcher => _fieldCatcher;
        public ObjectsContainer ObjectsContainer => _objectsContainer;
        public SystemCoordinator SystemCoordinator => _systemCoordinator;
        
        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _objectSpawner);

            Context.TryGetComponentsFromContainer(out _pointerProviders);

            Context.TryGetComponentFromContainer(out _fieldCatcher);

            Context.TryGetComponentFromContainer(out _objectsContainer);
            
            ObjectFinder.TryFindObjectByType(out _systemCoordinator);
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
            FillTheCatcher(_systemCoordinator.gameObject.GetCancellationTokenOnDestroy()).Forget();
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