using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Common.Repositories;
using _Project.Scripts.Features.Controls.Gyroscope;
using _Project.Scripts.Features.Controls.Pointer.MouseProvider;
using _Project.Scripts.Features.Controls.Pointer.Touch;
using _Project.Scripts.Features.Dimensions.Scale.ScaleProvider;
using _Project.Scripts.Features.Effects.Objects.EffectObjectsContainer;
using _Project.Scripts.Features.Effects.Providers.SplitSpriteEffectProvider;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider;
using _Project.Scripts.Features.Lifecycle.Destroyers.ClickObjectDestroyer;
using _Project.Scripts.Features.Lifecycle.LifecycleManager;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner;
using _Project.Scripts.Features.Physics.Engine;
using _Project.Scripts.Features.Physics.Forces.ExplosionForceProvider;
using _Project.Scripts.Features.Physics.Forces.GravityForceProvider;
using _Project.Scripts.Features.Physics.Services.Explosions.ExplosionProvider;
using _Project.Scripts.Features.Physics.Services.Gyroscope.GyroscopeGravityChanger;
using _Project.Scripts.Features.Random;
using Unity.Profiling;
using UnityEngine;

namespace _Project.Scripts.Bootstrap
{
    public class SystemCoordinator : MonoBehaviour
    {
        [SerializeField] private SystemConfig _systemConfig;
        
        private List<IUpdatableFeature> _updatableFeatures;
        private Dictionary<IUpdatableFeature, ProfilerMarker> _updatableFeaturesMarkers;
        
        private List<IFixedUpdatableFeature> _fixedUpdatableFeatures;
        private Dictionary<IFixedUpdatableFeature, ProfilerMarker> _fixedUpdatableFeaturesMarkers;
        
        public Context<BaseFeature> Context { get; private set; }
        public SystemConfig SystemConfig => _systemConfig;
        
        private void Awake()
        {
            Setup();
        }

        private void OnDestroy()
        {
            DestroyDestroyableFeatures();
            
            Context?.Clear();
            _updatableFeatures?.Clear();
            _updatableFeaturesMarkers?.Clear();
            _fixedUpdatableFeatures?.Clear();
            _fixedUpdatableFeaturesMarkers?.Clear();
        }
        
        private void Setup()
        {
            SetupContext();
            SetupFeatures();
            SetupUpdatableFeatures();
            SetupFixedUpdatableFeatures();
        }

        private void SetupContext()
        {
            Context = new Context<BaseFeature>();
            Context.SetCancellationToken(destroyCancellationToken);
        }

        private void SetupFeatures()
        {
            Context.AddFeatureWithConfig(new PhysicsEngine(), _systemConfig.PhysicsEngineConfig);
            Context.AddFeatureWithConfig(new GravityForceProvider(), _systemConfig.GravityForceProviderConfig);
            Context.AddFeatureWithConfig(new ExplosionForceProvider(), _systemConfig.ExplosionForceProviderConfig);
            
            Context.AddFeatureWithConfig(new CameraFieldProvider(), _systemConfig.CameraFieldProviderConfig);
            Context.AddFeatureWithConfig(new ColliderFieldCatcher(), _systemConfig.ColliderFieldCatcherConfig);
            Context.AddFeatureWithConfig(new RandomProvider(), _systemConfig.RandomProviderConfig);
            Context.AddFeatureWithConfig(new ObjectsContainer(), _systemConfig.ObjectsContainerConfig);
            Context.AddFeatureWithConfig(new ExplosionProvider(), _systemConfig.ExplosionProviderConfig);
            
            Context.AddFeature(new TouchProvider());
            Context.AddFeature(new MouseProvider());
            Context.AddFeature(new GyroscopeProvider());
            
            Context.AddFeatureWithConfig(new ScaleProvider(), _systemConfig.ScaleProviderConfig);
            
            Context.AddFeatureWithConfig(new FieldCatcherSpawner(), _systemConfig.PhysicsObjectSpawnerConfig);
            Context.AddFeatureWithConfig(new ClickObjectDestroyer(), _systemConfig.ClickObjectDestroyerConfig);
            
            Context.AddFeatureWithConfig(new GyroscopeGravityChanger(), _systemConfig.GyroscopeGravityChangerConfig);
            
            Context.AddFeatureWithConfig(new EffectObjectsContainer(), _systemConfig.EffectObjectsContainerConfig);
            Context.AddFeatureWithConfig(new SplitSpriteEffectProvider(), _systemConfig.SplitSpriteEffectProviderConfig);
            
            Context.AddFeatureWithConfig(new LifecycleManager(), _systemConfig.LifecycleManagerConfig);
        }

        private void SetupUpdatableFeatures()
        {
            _updatableFeatures = Context.Container.OfType<IUpdatableFeature>().ToList();
            _updatableFeaturesMarkers = new();
            
            foreach (var updatableFeature in _updatableFeatures)
            {
                _updatableFeaturesMarkers.TryAdd(updatableFeature, 
                    new ProfilerMarker(updatableFeature.GetType().Name));
            }
        }

        private void SetupFixedUpdatableFeatures()
        {
            _fixedUpdatableFeatures = Context.Container.OfType<IFixedUpdatableFeature>().ToList();
            _fixedUpdatableFeaturesMarkers = new();

            foreach (var fixedUpdatableFeature in _fixedUpdatableFeatures)
            {
                _fixedUpdatableFeaturesMarkers.TryAdd(fixedUpdatableFeature, 
                    new ProfilerMarker(fixedUpdatableFeature.GetType().Name));
            }
        }

        private void Update()
        {
            foreach (var updatableFeature in _updatableFeatures)
            {
                if (!_updatableFeaturesMarkers.TryGetValue(updatableFeature, out var marker))
                {
                    updatableFeature.Update();
                    continue;
                }

                using (marker.Auto())
                {
                    updatableFeature.Update();
                }
            }
        }

        private void FixedUpdate()
        {
            foreach (var fixedUpdatableFeature in _fixedUpdatableFeatures)
            {
                if (!_fixedUpdatableFeaturesMarkers.TryGetValue(fixedUpdatableFeature, out var marker))
                {
                    fixedUpdatableFeature.FixedUpdate();
                    continue;
                }

                using (marker.Auto())
                {
                    fixedUpdatableFeature.FixedUpdate();
                }
            }
        }

        private void DestroyDestroyableFeatures()
        {
            var destroyableFeatures = Context.Container.OfType<IDestroyableFeature>().ToList();
            
            foreach (var destroyableFeature in destroyableFeatures)
            {
                destroyableFeature.OnDestroy();
            }
        }
    }
}