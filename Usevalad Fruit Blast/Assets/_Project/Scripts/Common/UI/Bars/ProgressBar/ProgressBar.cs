using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Common.UI.Bars.ProgressBar
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ProgressLine _progressLine;
        [SerializeField] private LevelBar.LevelBar _levelBar;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private TextMeshProUGUI _progressPercentageText;
        [SerializeField] private Transform _progressTarget;
        
        [Header("Progress")]
        [SerializeField] private int _startLevel = 1;
        [SerializeField] private int _startLevelMaxExperience = 200;
        [SerializeField] private ProgressGrowthType _progressGrowthType = ProgressGrowthType.Linear;
        [SerializeField] private int _experienceGrowthRate = 2;
        [SerializeField] private float _updateDuration = 0.2f;
        [SerializeField] private Ease ease = Ease.InElastic;
        
        private int _currentExperience;
        private int _currentLevelMaxExperience;
        private int _experienceQueue;
        
        private float _animatedProgress;

        private CancellationToken _ct;
        
        public Transform ProgressTarget => _progressTarget;
        public bool IsProgressBarOnUpdate { get; private set; } = false;
        public LevelBar.LevelBar LevelBar => _levelBar;

        private void Awake()
        {
            _ct = gameObject.GetCancellationTokenOnDestroy();
            ExperienceUpdateListener().Forget();
        }
        
        public void Reset()
        {
            _levelBar.Setup(_startLevel);
            _currentExperience = 0;
            _currentLevelMaxExperience = _startLevelMaxExperience;
            SetProgressInstant(0);
        }

        public void AddExperience(int amount)
        {
            _experienceQueue += amount;
        }

        private async UniTaskVoid ExperienceUpdateListener()
        {
            while (!_ct.IsCancellationRequested)
            {
                if (_experienceQueue == 0)
                {
                    await UniTask.WaitForSeconds(_updateDuration, cancellationToken: _ct);
                    continue;
                }
                
                await UpdateExperience();
            }
        }
        
        private async UniTask UpdateExperience()
        {
            IsProgressBarOnUpdate = true;
            
            _currentExperience += _experienceQueue;
            _experienceQueue = 0;

            while (_currentExperience >= _currentLevelMaxExperience)
            {
                await UpdateProgressAnimatedAsync(1f);
        
                _currentExperience -= _currentLevelMaxExperience;
                _levelBar.LevelUp();
                UpdateCurrentLevelMaxExperience();

                SetProgressInstant(0f);
            }

            while (_currentExperience < 0 && _levelBar.Level > _startLevel)
            {
                await UpdateProgressAnimatedAsync(0f);
                
                _levelBar.LevelDown();
                UpdateCurrentLevelMaxExperience();
                _currentExperience += _currentLevelMaxExperience;
                
                SetProgressInstant(1f);
            }

            if (_currentExperience < 0)
            {
                _currentExperience = 0;
            }

            await UpdateProgressAnimatedAsync((float)_currentExperience / _currentLevelMaxExperience);
            
            IsProgressBarOnUpdate = false;
        }
        
        private async UniTask UpdateProgressAnimatedAsync(float targetProgress)
        {
            targetProgress = Mathf.Clamp01(targetProgress);

            var startValue = _animatedProgress;
            var endValue = targetProgress;
            var progressLine = _progressLine;
            var duration = _updateDuration;
            var localEase = ease;
            var token = _ct;

            await DOTween.To(
                    () => startValue,
                    x => {
                        startValue = x;
                        _animatedProgress = x;
                        progressLine.Progress = x;
                        UpdateProgressTextInterpolated(x);
                    },
                    endValue,
                    duration
                )
                .SetEase(localEase)
                .OnComplete(() => {
                    _animatedProgress = endValue;
                    UpdateProgressTextInterpolated(endValue);
                })
                .WithCancellation(token);
        }
        
        private void UpdateProgressTextInterpolated(float progress)
        {
            var experience = Mathf.RoundToInt(progress * _currentLevelMaxExperience);
            _progressText.text = $"{experience.ToString()} / {_currentLevelMaxExperience.ToString()}";
            _progressPercentageText.text = $"{Mathf.RoundToInt(progress * 100f).ToString()} %";
        }
        
        private void SetProgressInstant(float progress)
        {
            progress = Mathf.Clamp01(progress);
            _animatedProgress = progress;
            _progressLine.Progress = progress;
            UpdateProgressTextInterpolated(progress);
        }
        
        private void UpdateCurrentLevelMaxExperience()
        {
            var baseValue = _startLevelMaxExperience;
            var levelDelta = _levelBar.Level - _startLevel;
            
            if (levelDelta == 0)
            {
                _currentLevelMaxExperience = baseValue;
                return;
            }
            
            var multiplier = _progressGrowthType switch
            {
                ProgressGrowthType.Linear => levelDelta,
                ProgressGrowthType.Exponential => Mathf.Pow(levelDelta, 2f),
                ProgressGrowthType.Logarithmic => Mathf.Log(levelDelta + 1),
                _ => 1f
            };

            _currentLevelMaxExperience = Mathf.RoundToInt(baseValue + multiplier * _experienceGrowthRate);
        }
    }
}