using System;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Spawners
{
    [Serializable]
    public class SpawnGroupsConfig : IFeatureConfig
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private bool _isActive = true;
        
        public int Id => _id;
        public Sprite Sprite => _sprite;
        public bool IsActive { get => _isActive; set => _isActive = value; }
    }
}