using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Difficulty.DifficultyScaler
{
    [CreateAssetMenu(fileName = "DifficultyScalerConfig", menuName = "Configs/Difficulty/Difficulty Scaler Config")]
    public class DifficultyScalerConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private int _minTypeCount = 3;
        [SerializeField] private int _maxTypeCount = 9;
        
        public int MinTypeCount => _minTypeCount;
        public int MaxTypeCount => _maxTypeCount;
    }
}