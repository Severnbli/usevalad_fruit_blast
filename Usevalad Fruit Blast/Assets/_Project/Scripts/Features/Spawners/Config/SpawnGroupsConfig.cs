using System;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners.Config
{
    [Serializable]
    public class SpawnGroupsConfig
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private bool _isActive = true;
        
        public Sprite Sprite => _sprite;
        public bool IsActive => _isActive;
    }
}