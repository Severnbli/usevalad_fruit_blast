using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Common.Serializers;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Random;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks.PerksProvider
{
    public class PerksProvider : BaseFeature, IConfigurableFeature<PerksProviderConfig>, IDestroyableFeature
    {
        private static readonly string _playerPrefsPerksName = "perks";
        
        private RandomProvider _randomProvider;
        
        public PerksProviderConfig PerksProviderConfig { get; private set; }
        public List<BasePerk> Perks { get; private set; }

        public override void Init()
        {
            base.Init();
            
            Context.TryGetComponentFromContainer(out _randomProvider);
            
            LoadPerks();
        }
        
        public void Configure(PerksProviderConfig perksProviderConfig)
        {
            PerksProviderConfig = perksProviderConfig;
        }
        
        public void OnDestroy()
        {
            SavePerks();
        }

        private void LoadPerks()
        {
            var perksString = PlayerPrefs.GetString(_playerPrefsPerksName, "{}");

            var perksArray = JsonSerializer.CollectionFromJson<BasePerk>(perksString);

            if (perksArray == null)
            {
                Perks = new List<BasePerk>();
            }
            else
            {
                Perks = perksArray.ToList();
            }
        }

        private void SavePerks()
        {
            var perksString = JsonSerializer.CollectionToJson(Perks.ToArray());
            
            PlayerPrefs.SetString(_playerPrefsPerksName, perksString);
        }

        public List<BasePerk> GetAllAvailablePerksByLevel(int level)
        {
            var allPerks = new List<BasePerk>();

            allPerks.AddRange(PerksProviderConfig.VolumeBombPerks?.Where(p => p.DiscoveryLevel <= level) ?? Enumerable.Empty<BasePerk>());
            allPerks.AddRange(PerksProviderConfig.VerticalBombPerks?.Where(p => p.DiscoveryLevel <= level) ?? Enumerable.Empty<BasePerk>());
            allPerks.AddRange(PerksProviderConfig.HorizontalBombPerks?.Where(p => p.DiscoveryLevel <= level) ?? Enumerable.Empty<BasePerk>());
            allPerks.AddRange(PerksProviderConfig.LuckyGuyPerks?.Where(p => p.DiscoveryLevel <= level) ?? Enumerable.Empty<BasePerk>());
            allPerks.AddRange(PerksProviderConfig.FastFingerPerks?.Where(p => p.DiscoveryLevel <= level) ?? Enumerable.Empty<BasePerk>());
            allPerks.AddRange(PerksProviderConfig.ToughNutPerks?.Where(p => p.DiscoveryLevel <= level) ?? Enumerable.Empty<BasePerk>());

            return allPerks;
        }

        public BasePerk GetRandomAvailablePerkByLevel(int level)
        {
            var availablePerks = GetAllAvailablePerksByLevel(level);
            
            return availablePerks[_randomProvider.Random.Next(0, availablePerks.Count)];
        }
    }
}