using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Random.Config;
using UnityEngine;

namespace _Project.Scripts.Features.Random
{
    public class RandomProvider : BaseFeature
    {
        [SerializeField] private int _seed;
        
        public int Seed { get => _seed; set => _seed = value; }
        public global::System.Random Random { get; private set; }
        
        public override void Init(IFeatureConfig config)
        {
            if (config is not RandomProviderConfig randomProviderConfig)
            {
                return;
            }
            
            _seed = randomProviderConfig.Seed;
            Random = new global::System.Random(_seed);
        }

        public void UpdateRandom(int seed)
        {
            _seed = seed;
            Random = new global::System.Random(_seed);
        }
    }
}