using System;
using _Project.Scripts.Common.UI.Bars.HealthBar;
using _Project.Scripts.Common.UI.Popups;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;
using ProgressBar = _Project.Scripts.Common.UI.Bars.ProgressBar.ProgressBar;

namespace _Project.Scripts.Features.UI.UIProvider
{
    [Serializable]
    public class UIProviderConfig : IFeatureConfig
    {
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private TextPopup _textPopup;
        [SerializeField] private PausePopup _pausePopup;
        
        public ProgressBar ProgressBar => _progressBar;
        public HealthBar HealthBar => _healthBar;
        public TextPopup TextPopup => _textPopup;
        public PausePopup PausePopup => _pausePopup;
    }
}