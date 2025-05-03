namespace _Project.Scripts.Features.Common
{
    public interface IConfigurableFeature<in T> where T : IFeatureConfig
    {
        public void Configure(T config);
    }
}