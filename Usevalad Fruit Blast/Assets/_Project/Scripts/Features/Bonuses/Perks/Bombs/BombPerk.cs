using System;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks.Bombs
{
    [Serializable]
    public class BombPerk : BasePerk
    {
        [SerializeField] private float _explosionStrength = 200f;

        public float ExplosionStrength => _explosionStrength;
    }
}