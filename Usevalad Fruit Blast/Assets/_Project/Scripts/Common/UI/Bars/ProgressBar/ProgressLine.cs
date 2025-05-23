using System;
using Cysharp.Threading.Tasks;
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

        private void Update()
        {
            UpdateBar();
        }

        public float Progress { get; set; }

        private void UpdateBar()
        {
            var totalWidth = _progressLine.rect.width;
            var leftWidth = _leftCap.rect.width;
            var rightWidth = _rightCap.rect.width;

            var fillArea = totalWidth * Progress;

            var isAreaEnough = fillArea >= leftWidth + rightWidth;

            _leftCap.gameObject.SetActive(isAreaEnough);
            _centerFill.gameObject.SetActive(isAreaEnough);
            _rightCap.gameObject.SetActive(isAreaEnough);

            if (!isAreaEnough)
            {
                _rightCap.transform.position = _leftCap.transform.position;
                return;
            }

            _leftCap.anchoredPosition = Vector2.zero;

            var centerWidth = Mathf.Max(0, fillArea - leftWidth - rightWidth);
            var centerTargetSize = new Vector2(centerWidth, _centerFill.sizeDelta.y);
            var centerTargetPos = new Vector2(leftWidth, 0f);
            var rightTargetPos = new Vector2(leftWidth + centerWidth, 0f);

            _centerFill.sizeDelta = centerTargetSize;
            _centerFill.anchoredPosition = centerTargetPos;
            _rightCap.anchoredPosition = rightTargetPos;
        }
    }
}
