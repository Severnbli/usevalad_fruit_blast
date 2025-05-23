using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Common.UI.Bars.HealthBar;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Lifecycle.Spawners;
using _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner;
using _Project.Scripts.Features.Random;
using _Project.Scripts.Features.Stats.Health;
using Sirenix.Utilities;
using UnityEngine;

namespace _Project.Scripts.Features.Difficulty.DifficultyScaler
{
    public class DifficultyScaler : DifficultyFeature, IConfigurableFeature<DifficultyScalerConfig>, 
        IDestroyableFeature, IResettableFeature
    {
        private FieldCatcherSpawner _fieldCatcherSpawner;
        private HealthProvider _healthProvider;
        private RandomProvider _randomProvider;
        
        private HealthBar _healthBar;
        private List<SpawnGroupsConfig> _spawnGroups;
        
        public DifficultyScalerConfig DifficultyScalerConfig { get; private set; }

        public override void Init()
        {
            base.Init();
            
            Context.TryGetComponentFromContainer(out _fieldCatcherSpawner);
            
            _spawnGroups = _fieldCatcherSpawner.FieldCatcherSpawnerConfig
                .PhysicsObjectSpawnerConfig.ObjectSpawnerConfig.SpawnGroups;
            
            Context.TryGetComponentFromContainer(out _healthProvider);
            Context.TryGetComponentFromContainer(out _randomProvider);
        }

        public void Configure(DifficultyScalerConfig difficultyScalerConfig)
        {
            DifficultyScalerConfig = difficultyScalerConfig;
            ConnectToHealthBar();
        }

        public void OnDestroy()
        {
            DisconnectFromHealthBar();
            _spawnGroups.ForEach(x => x.IsActive = false);
            SetAvailableTypes(1);
        }
        
        public void Reset()
        {
            _spawnGroups.ForEach(x => x.IsActive = false);
            SetAvailableTypes(_healthProvider.HealthProviderConfig.MaxHealth);
        }
        
        private void ConnectToHealthBar()
        {
            _healthBar = _healthProvider.HealthBar;
            _healthBar.OnHealthUpdate += SetAvailableTypes;
            SetAvailableTypes(_healthProvider.HealthProviderConfig.MaxHealth);
        }

        private void DisconnectFromHealthBar()
        {
            if (_healthBar != null)
            {
                _healthBar.OnHealthUpdate -= SetAvailableTypes;
            }
        }
        
        private void SetAvailableTypes(int currentHealth)
        {
            if (_spawnGroups.Count == 0)
            {
                return;
            }

            var maxAllowed = _spawnGroups.Count;

            var clampedMin = Mathf.Clamp(DifficultyScalerConfig.MinTypeCount, 0, maxAllowed);
            var clampedMax = Mathf.Clamp(DifficultyScalerConfig.MaxTypeCount, clampedMin, maxAllowed);

            var t = 1f - (float)currentHealth / _healthProvider.HealthProviderConfig.MaxHealth;
            var countOfTypes = Mathf.RoundToInt(Mathf.Lerp(clampedMin, clampedMax, t));
            
            var alreadyActive = _spawnGroups.Where(x => x.IsActive).ToList();
            
            var needToActivate = countOfTypes - alreadyActive.Count;

            if (needToActivate < 0)
            {
                alreadyActive
                    .Take(Mathf.Abs(needToActivate))
                    .ForEach(x => x.IsActive = false);
            }

            var remaining = _spawnGroups
                .Where(x => !x.IsActive)
                .OrderBy(_ => _randomProvider.Random.Next())
                .Take(needToActivate);
            
            remaining.ForEach(x => x.IsActive = true);
        }
    }
}