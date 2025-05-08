using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Forces
{
    [CreateAssetMenu(fileName = "ForceProviderConfig", menuName = "Configs/Physics/Forces/Force Provider Config")]
    public class ForceProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private float _factor;
        [SerializeField] private Vector2 _direction;
        
        public float Factor => _factor;
        public Vector2 Direction => _direction;
    }
}