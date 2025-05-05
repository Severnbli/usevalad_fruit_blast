using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner
{
    [CreateAssetMenu(fileName = "PhysicsObjectSpawnerConfig", menuName = "Configs/Spawners/Physics Object Spawner/Physicsc Object Spawner Config")]
    public class PhysicsObjectSpawnerConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private ObjectSpawnerConfig _objectSpawnerConfig;
        [SerializeField] private float _minMass = 1f;
        [SerializeField] private float _maxMass = 1f;
        [SerializeField] private float _minBounciness = 0.8f;
        [SerializeField] private float _maxBounciness = 0.8f;
        [SerializeField] private float _minGravityFactor = 1f;
        [SerializeField] private float _maxGravityFactor = 1f;
        [SerializeField] private Vector2 _minStartVelocity;
        [SerializeField] private Vector2 _maxStartVelocity;
        
        public ObjectSpawnerConfig ObjectSpawnerConfig => _objectSpawnerConfig;
        public float MinMass => _minMass;
        public float MaxMass => _maxMass;
        public float MinBounciness => _minBounciness;
        public float MaxBounciness => _maxBounciness;
        public float MinGravityFactor => _minGravityFactor;
        public float MaxGravityFactor => _maxGravityFactor;
        public Vector2 MinStartVelocity => _minStartVelocity;
        public Vector2 MaxStartVelocity => _maxStartVelocity;
    }
}