using System;
using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks.Experience
{
    [Serializable]
    public class LuckyGuyPerk : ExperiencePerk
    {
        [SerializeField] protected float _experienceMultiplier;
        
        public float ExperienceMultiplier => _experienceMultiplier;
        public override void UsePerk(PerkableObject perkableObject)
        {
            
        }
    }
}