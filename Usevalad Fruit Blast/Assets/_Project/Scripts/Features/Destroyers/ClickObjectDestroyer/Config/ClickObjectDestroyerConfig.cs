using System;
using _Project.Scripts.Features.Destroyers.Config;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.ClickObjectDestroyer.Config
{
    [Serializable]
    public class ClickObjectDestroyerConfig : BaseObjectDestroyerConfig
    {
        [SerializeField] private float _destroyDistance;
        
        public float DestroyDistance => _destroyDistance;
    }
}