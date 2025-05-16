using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Common.UI.Bars.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthAmount;
        [SerializeField] private RectTransform _pulseTarget;
        [SerializeField] private float _pulseScale = 1.1f;
        [SerializeField] private float _pulseDuration = 0.3f;

        public void SetHealth(int amount)
        {
            _healthAmount.text = amount.ToString();
            PulseAnimation();
        }
        
        private void PulseAnimation()
        {
            _pulseTarget?.DOPunchScale(Vector3.one * (_pulseScale - 1f), _pulseDuration, 1, 0.5f);
        }
    }
}