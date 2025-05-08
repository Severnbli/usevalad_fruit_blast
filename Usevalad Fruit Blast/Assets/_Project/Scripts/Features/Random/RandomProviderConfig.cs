using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Random
{
    [CreateAssetMenu(fileName = "RandomProviderConfig", menuName = "Configs/Random/Random Provider Config")]
    public class RandomProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private int _seed;
        
        public int Seed => _seed;
    }
}