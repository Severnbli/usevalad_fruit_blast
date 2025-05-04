using _Project.Scripts.Features.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.LifecycleManager
{
    [CreateAssetMenu(fileName = "LifecycleManagerConfig", menuName = "Configs/Lifecycle/Lifecycle Manager Config")]
    public class LifecycleManagerConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private float _maxCorruptedFieldCatcherArea = 0.8f;
        [SerializeField] private float _timeToFillFieldCatcherArea = 0.3f;
        [InfoBox("Sensitive times - important periods of the game cycle, such as starting, losing and etc.")]
        [SerializeField] private bool _isPointersAvailableOnSensitiveTimes = false;
        
        public float MaxCorruptedFieldCatcherArea => _maxCorruptedFieldCatcherArea;
        public float TimeToFillFieldCatcherArea => _timeToFillFieldCatcherArea;
        public bool IsPointersAvailableOnSensitiveTimes => _isPointersAvailableOnSensitiveTimes;
    }
}