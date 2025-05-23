using System;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks
{
    [Serializable]
    public abstract class BasePerk
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] protected int _level = 1;
        [SerializeField] protected float _occurrenceChance = 0.2f;
        [SerializeField] protected float _triggerChance = 0.6f;
        [SerializeField] protected int _discoveryLevel = 1;
        [SerializeField] protected float _triggeringCooldown = 0f;
        
        public Sprite Icon => _icon;
        public string Name => _name;    
        public int Level => _level;
        public float OccurrenceChance => _occurrenceChance;
        public float TriggerChance => _triggerChance;
        public int DiscoveryLevel => _discoveryLevel;
        public float TriggeringCooldown => _triggeringCooldown;

        public abstract void UsePerk(PerkableObject perkableObject);
    }
}