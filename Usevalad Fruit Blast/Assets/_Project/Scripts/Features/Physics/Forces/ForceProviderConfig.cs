using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Forces
{
    [CreateAssetMenu(fileName = "ForceProviderConfig", menuName = "Configs/Physics/Forces/Force Provider Config")]
    public class ForceProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private float _factor;
        [SerializeField] private Vector2 _direction;
        
        public float Factor { get => _factor; set => _factor = value; }
        public Vector2 Direction { get => _direction; set => _direction = value; }
    }
}