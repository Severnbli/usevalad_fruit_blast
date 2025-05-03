using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Random
{
    public class RandomProvider : BaseFeature, IConfigurableFeature<RandomProviderConfig>
    {
        [SerializeField] private RandomProviderConfig _randomProviderConfig;
        
        public global::System.Random Random { get; private set; }
        
        public void Configure(RandomProviderConfig randomProviderConfig)
        {
            UpdateRandom(_randomProviderConfig.Seed);
        }

        public void UpdateRandom(int seed)
        {
            Random = new global::System.Random(seed);
        }
    }
}