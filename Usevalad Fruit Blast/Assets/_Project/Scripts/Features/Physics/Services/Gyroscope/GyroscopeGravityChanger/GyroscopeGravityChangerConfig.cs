using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Gyroscope.GyroscopeGravityChanger
{
    [CreateAssetMenu(fileName = "GyroscopeGravityChangerConfig", menuName = "Configs/Physics/Services/Gyroscope/Gyroscope Gravity Changer Config")]
    public class GyroscopeGravityChangerConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private float _maxAngle;
        
        public float MaxAngle => _maxAngle;
    }
}