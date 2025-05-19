using System;
using _Project.Scripts.Common.UI.Bars.HealthBar;
using _Project.Scripts.Features.FeatureCore;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Health
{
    [Serializable]
    public class HealthProviderConfig : IFeatureConfig
    {
        [SerializeField] private int _maxHealth = 5;
        [SerializeField] private HealthBar[] _healthBars;
        
        public int MaxHealth => _maxHealth;
        public HealthBar[] HealthBars => _healthBars;
    }
}