using System;
using _Project.Scripts.Features.Bonuses.BonusesCore;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks.Turns
{
    [Serializable]
    public abstract class TurnBasedPerk : BasePerk
    {
        [SerializeField] protected int _turnsAmount;
        
        public int TurnsAmount => _turnsAmount;
    }
}