using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners
{
    public abstract class ObjectSpawner : BaseFeature
    {
        [SerializeField] protected GameObject _prefab;
        
        public GameObject Prefab => _prefab;
        
        public abstract void Spawn();
    }
}