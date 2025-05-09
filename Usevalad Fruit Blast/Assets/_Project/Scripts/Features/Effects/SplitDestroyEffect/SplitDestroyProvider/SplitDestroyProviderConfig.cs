using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.SplitDestroyEffect.SplitDestroyProvider
{
    [CreateAssetMenu(fileName = "SplitDestroyProviderConfig", menuName = "Configs/Effects/Split Destroy Provider Config")]
    public class SplitDestroyProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private int _splitX = 1;
        [SerializeField] private int _splitY = 1;
        [SerializeField] private float _destroyForce = 5f;
        
        public int SplitX => _splitX;
        public int SplitY => _splitY;
        public float DestroyForce => _destroyForce;
    }
}