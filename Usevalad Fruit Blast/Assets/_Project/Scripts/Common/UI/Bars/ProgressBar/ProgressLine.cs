using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Common.UI.Bars.ProgressBar
{
    public class ProgressLine : MonoBehaviour
    {
        [SerializeField] private RectTransform _progressLine;
        [SerializeField] private RectTransform _leftCap;
        [SerializeField] private RectTransform _centerFill;
        [SerializeField] private RectTransform _rightCap;
        [SerializeField] private float animationDuration = 0.3f;
        [SerializeField] private Ease ease = Ease.InElastic;
        
        private float _previousProgress;
        private float _leftCapWidth;
        private float _rightCapWidth;

        private void LateUpdate()
        {
            UpdateBar();
        }

        public bool TryUpdateProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            
            if (Mathf.Approximately(_previousProgress, progress))
            {
                return false;
            }
            
            _previousProgress = progress;
            return true;
        }

        private void UpdateBar()
        {
            var totalWidth = _progressLine.rect.width;
            var leftWidth = _leftCap.rect.width;
            var rightWidth = _rightCap.rect.width;

            var fillArea = totalWidth * _previousProgress;

            var isAreaEnough = fillArea >= leftWidth + rightWidth;
            
            _leftCap.gameObject.SetActive(isAreaEnough);
            _centerFill.gameObject.SetActive(isAreaEnough);
            _rightCap.gameObject.SetActive(isAreaEnough);

            if (!isAreaEnough)
            {
                return;
            }
            
            _leftCap.anchoredPosition = Vector2.zero;

            var centerWidth = Mathf.Max(0, fillArea - leftWidth - rightWidth);
            var centerTargetSize = new Vector2(centerWidth, _centerFill.sizeDelta.y);
            var centerTargetPos = new Vector2(leftWidth, 0f);

            _centerFill.DOSizeDelta(centerTargetSize, animationDuration).SetEase(ease);
            _centerFill.DOAnchorPos(centerTargetPos, animationDuration).SetEase(ease);

            var rightTargetPos = new Vector2(leftWidth + centerWidth, 0f);
            _rightCap.DOAnchorPos(rightTargetPos, animationDuration).SetEase(ease);
        }
    }
}