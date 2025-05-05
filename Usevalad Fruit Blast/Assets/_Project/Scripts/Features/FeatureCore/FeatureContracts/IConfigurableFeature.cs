namespace _Project.Scripts.Features.FeatureCore.FeatureContracts
{
    public interface IConfigurableFeature<in T> where T : IFeatureConfig
    {
        public void Configure(T config);
    }
}