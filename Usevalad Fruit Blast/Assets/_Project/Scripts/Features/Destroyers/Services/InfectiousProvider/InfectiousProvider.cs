using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Destroyers.Services.InfectiousProvider.Config;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.Services.InfectiousProvider
{
    public class InfectiousProvider: BaseFeature
    {
        [SerializeField] private float _infectiousDistance = 0.5f;
        
        public float InfectiousDistance => _infectiousDistance;
        
        public override void Init(IFeatureConfig config)
        {
            if (config is not InfectiousProviderConfig infectiousProviderConfig)
            {
                return;
            }
            
            _infectiousDistance = infectiousProviderConfig.InfectiousDistance;
        }
    }
}