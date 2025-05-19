using System;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.BonusesCore
{
    [Serializable]
    public abstract class BasePerk
    {
        [SerializeField] protected int _level = 1;
        [SerializeField] protected float _occurrenceChance = 0.2f;
        [SerializeField] protected float _triggerChance = 0.6f;
        [SerializeField] protected int _discoveryLevel = 1;
        [SerializeField] protected float _triggeringCooldown = 0f;
        
        public int Level => _level;
        public float OccurrenceChance => _occurrenceChance;
        public float TriggerChance => _triggerChance;
        public int DiscoveryLevel => _discoveryLevel;
        public float TriggeringCooldown => _triggeringCooldown;
    }
}