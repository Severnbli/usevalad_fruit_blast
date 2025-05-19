using _Project.Scripts.Features.FeatureCore.FeatureContracts;

namespace _Project.Scripts.Features.Stats.Health.HealthInfluencers
{
    public abstract class HealthInfluencer : StatsFeature, IConfigurableFeature<HealthInfluencerConfig>
    {
        protected HealthProvider _healthProvider;
        
        public HealthInfluencerConfig HealthInfluenceConfig { get; private set; }

        public override void Init()
        {
            base.Init();
            
            Context.TryGetComponentFromContainer(out _healthProvider);
        }

        public void Configure(HealthInfluencerConfig healthInfluencerConfig)
        {
            HealthInfluenceConfig = healthInfluencerConfig;
        }

        protected abstract void Influence();
    }
}