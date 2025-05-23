using UnityEngine;

namespace _Project.Scripts.Features.Bonuses.Perks
{
    public class PerkableObject : MonoBehaviour
    {
        public BasePerk perk { get; set; }

        public void UsePerk()
        {
            perk.UsePerk(this);
        }
    }
}