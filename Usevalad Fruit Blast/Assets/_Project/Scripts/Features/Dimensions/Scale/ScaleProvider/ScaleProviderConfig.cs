using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Dimensions.Scale.ScaleProvider
{
    [CreateAssetMenu(fileName = "ScaleProviderConfig", menuName = "Configs/Dimensions/Scale/Scale Provider Config")]
    public class ScaleProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private Vector2 _benchmarkSize = new Vector2(3.5f, 7.5f);
        
        public Vector2 BenchmarkSize => _benchmarkSize;
    }
}