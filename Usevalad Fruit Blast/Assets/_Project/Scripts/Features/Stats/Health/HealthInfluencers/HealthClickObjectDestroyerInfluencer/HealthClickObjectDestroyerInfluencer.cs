using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Lifecycle.Destroyers.ClickObjectDestroyer;

namespace _Project.Scripts.Features.Stats.Health.HealthInfluencers.HealthClickObjectDestroyerInfluencer
{
    public class HealthClickObjectDestroyerInfluencer : HealthInfluencer, IDestroyableFeature
    {
        private ClickObjectDestroyer _clickObjectDestroyer;

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _clickObjectDestroyer);
            
            ConnectHealthToInfluencer();
        }

        public void OnDestroy()
        {
            DisconnectHealthFromInfluencer();
        }

        private void ConnectHealthToInfluencer()
        {
            if (_clickObjectDestroyer == null)
            {
                return;
            }

            _clickObjectDestroyer.OnObjectsDestroy += Influence;
        }

        private void DisconnectHealthFromInfluencer()
        {
            if (_clickObjectDestroyer == null)
            {
                return;
            }
            
            _clickObjectDestroyer.OnObjectsDestroy -= Influence;
        }

        protected override void Influence()
        {
            _healthProvider.DecreaseHealth(HealthInfluenceConfig.HealthInfluenceAmount);
        }
    }
}