using System;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider
{
    [Serializable]
    public class ExperienceEffectGroup
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _minExperienceAmount;
        [SerializeField] private int _maxExperienceAmount;
        [SerializeField] private float _probability;
        [SerializeField] private bool _isActive;
        
        public Sprite Icon => _icon;
        public int MinExperienceAmount => _minExperienceAmount;
        public int MaxExperienceAmount => _maxExperienceAmount;
        public float Probability => _probability;
        public bool IsActive => _isActive;
    }
}