using System;
using _Project.Scripts.Features.FeatureCore;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Health
{
    [Serializable]
    public class HealthFeatureConfig : IFeatureConfig
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private int _maxHealth = 5;
        
        public TextMeshProUGUI HealthText => _healthText;
        public int MaxHealth => _maxHealth;
    }
}