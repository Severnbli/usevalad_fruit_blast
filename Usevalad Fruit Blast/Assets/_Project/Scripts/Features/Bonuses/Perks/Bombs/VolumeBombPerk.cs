using System;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks.Bombs
{
    [Serializable]
    public class VolumeBombPerk : BasePerk
    {
        [SerializeField] protected float _radius;
        
        public float Radius => _radius;
        public override void UsePerk(PerkableObject perkableObject)
        {
            
        }
    }
}