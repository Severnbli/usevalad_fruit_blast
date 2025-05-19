using System;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks.Bombs
{
    [Serializable]
    public class AxisBombPerk : BombPerk
    {
        [SerializeField] protected float _lineOffset;
        
        public float LineOffset => _lineOffset;
    }
}