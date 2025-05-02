using System;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners.Config
{
    [Serializable]
    public class SpawnGroupsConfig
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private bool _isActive = true;
        
        public int Id => _id;
        public Sprite Sprite => _sprite;
        public bool IsActive => _isActive;
    }
}