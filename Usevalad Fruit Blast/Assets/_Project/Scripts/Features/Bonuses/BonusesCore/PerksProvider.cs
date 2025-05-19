using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Common.Serializer;
using _Project.Scripts.Features.Bonuses.Perks;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.BonusesCore
{
    public class PerksProvider : BaseFeature, IDestroyableFeature
    {
        private static readonly string _playerPrefsPerksName = "perks";
        
        public List<BasePerk> Perks { get; private set; }

        public override void Init()
        {
            base.Init();
            
            LoadPerks();
        }
        
        public void OnDestroy()
        {
            SavePerks();
        }

        private void LoadPerks()
        {
            var perksString = PlayerPrefs.GetString(_playerPrefsPerksName, "{}");
            
            Perks = JsonSerializer.CollectionFromJson<BasePerk>(perksString).ToList();
        }

        private void SavePerks()
        {
            var perksString = JsonSerializer.CollectionToJson(Perks.ToArray());
            
            PlayerPrefs.SetString(_playerPrefsPerksName, perksString);
        }
    }
}