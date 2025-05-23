using System;
using _Project.Scripts.Common.UI.Bars.ProgressBar;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.UI.UIProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Stats.Experience
{
    public class ExperienceProvider : BaseFeature, IConfigurableFeature<ExperienceProviderConfig>, IResettableFeature
    {
        private UIProvider _uiProvider;
        
        public ExperienceProviderConfig ExperienceProviderConfig { get; private set; }
        public ProgressBar ProgressBar { get; private set; }
        public Transform ProgressTarget { get; private set; }

        public override void Init()
        {
            base.Init();

            if (Context.TryGetComponentFromContainer(out _uiProvider))
            {
                ProgressBar = _uiProvider.UIProviderConfig.ProgressBar;
                ProgressTarget = _uiProvider.UIProviderConfig.ProgressBar.ProgressTarget; 
            }
        }
        
        public void Configure(ExperienceProviderConfig experienceProviderConfig)
        {
            ExperienceProviderConfig = experienceProviderConfig;
            
            Reset();
        }
        
        public void Reset()
        {
            ProgressBar?.Reset();
        }

        public void AddExperience(int experience)
        {
            ProgressBar?.AddExperience(experience);
        }
    }
}