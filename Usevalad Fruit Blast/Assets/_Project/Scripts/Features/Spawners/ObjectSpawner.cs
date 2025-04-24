using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Random;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners
{
    public abstract class ObjectSpawner : BaseFeature
    {
        protected RandomProvider _randomProvider;
        
        public RandomProvider RandomProvider => _randomProvider;
        
        public abstract void Spawn();
        
        public override void Init(IFeatureConfig config)
        {
            _randomProvider = Context.Container.GetComponent<RandomProvider>();

            if (_randomProvider == null)
            {
                Debug.LogError("Check system priority setup: random provider must be earlier than object spawner!");
            }
        }
    }
}