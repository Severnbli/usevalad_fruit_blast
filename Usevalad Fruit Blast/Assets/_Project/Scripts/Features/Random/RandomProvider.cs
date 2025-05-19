using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;

namespace _Project.Scripts.Features.Random
{
    public class RandomProvider : BaseFeature, IConfigurableFeature<RandomProviderConfig>
    {
        private RandomProviderConfig _randomProviderConfig;
        
        public global::System.Random Random { get; private set; }
        
        public void Configure(RandomProviderConfig randomProviderConfig)
        {
            _randomProviderConfig = randomProviderConfig;
            UpdateRandom(_randomProviderConfig.Seed);
        }

        public void UpdateRandom(int seed)
        {
            Random = new global::System.Random(seed);
        }

        public float GetRandomInRange(float min, float max)
        {
            return (float) Random.NextDouble() * (max - min) + min;
        }
    }
}