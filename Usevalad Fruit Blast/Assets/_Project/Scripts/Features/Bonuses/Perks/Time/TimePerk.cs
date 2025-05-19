using System;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks.Time
{
    [Serializable]
    public abstract class TimePerk
    {
        [SerializeField] protected float _duration;
        
        public float Duration => _duration;
    }
}