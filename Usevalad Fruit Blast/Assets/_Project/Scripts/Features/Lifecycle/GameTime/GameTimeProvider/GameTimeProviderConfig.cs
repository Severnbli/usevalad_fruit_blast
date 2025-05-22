using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.GameTime.GameTimeProvider
{
    [CreateAssetMenu(fileName = "GameTimeProviderConfig", menuName = "Configs/Lifecycle/Game Time/Game Time Provider Config")]
    public class GameTimeProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private float _baseTime = 1f;
        
        public float BaseTime => _baseTime;
    }
}