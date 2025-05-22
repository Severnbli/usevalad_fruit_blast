using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Common.UI.Bars.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthAmount;
        [SerializeField] private RectTransform _pulseTarget;
        [SerializeField] private float _idlePulseScale = 1.05f;
        [SerializeField] private float _idlePulseDuration = 1f;
        [SerializeField] private float _changePulseScale = 1.1f;
        [SerializeField] private float _changePulseDuration = 0.3f;

        private Tween _idleTween;
        
        private void Awake()
        {
            StartIdlePulse();
        }

        private void OnDestroy()
        {
            _idleTween?.Kill();
        }

        public void SetHealth(int amount)
        {
            _healthAmount.text = amount.ToString();
            AnimateChangePulse();
        }

        private void StartIdlePulse()
        {
            _idleTween?.Kill();
            _idleTween = _pulseTarget.DOScale(Vector3.one * _idlePulseScale, _idlePulseDuration / 2)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void AnimateChangePulse()
        {
            _pulseTarget.DOKill();
            _pulseTarget.localScale = Vector3.one;
            _pulseTarget.DOPunchScale(Vector3.one * (_changePulseScale - 1f), _changePulseDuration, 1, 0.5f)
                .OnComplete(() => StartIdlePulse());
        }
    }
}