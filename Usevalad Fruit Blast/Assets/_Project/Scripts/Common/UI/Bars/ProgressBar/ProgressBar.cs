using TMPro;
using UnityEngine;

namespace _Project.Scripts.Common.UI.Bars.ProgressBar
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private ProgressLine _progressLine;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private TextMeshProUGUI _progressPercentageText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Transform _progressTarget;
        
        public Transform ProgressTarget => _progressTarget;
        
        public void SetLevel(int level)
        {
            _levelText.text = level.ToString();
        }

        public void SetProgress(int currentProgress, int maxProgress)
        {
            var progress = (float) currentProgress / maxProgress;
            
            _progressLine.TryUpdateProgress(progress);
            
            _progressText.text = $"{currentProgress.ToString()} / {maxProgress.ToString()}";
            _progressPercentageText.text = $"{Mathf.RoundToInt(progress * 100f).ToString()} %";
        }
    }
}