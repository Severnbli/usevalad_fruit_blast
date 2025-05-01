using _Project.Scripts.Features.Spawners.Config;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners.PhysicsObjectSpawner.Config
{
    [CreateAssetMenu(fileName = "PhysicsSpawnerConfig", menuName = "Configs/Spawners/Physics Object Spawner/Physicsc Group Object Config")]
    public class PhysicsSpawnerConfig : ScriptableObject
    {
        [SerializeField] private float _minMass = 1f;
        [SerializeField] private float _maxMass = 1f;
        [SerializeField] private float _minBounciness = 0.8f;
        [SerializeField] private float _maxBounciness = 0.8f;
        [SerializeField] private float _minGravityFactor = 1f;
        [SerializeField] private float _maxGravityFactor = 1f;
        [SerializeField] private Vector2 _minStartVelocity;
        [SerializeField] private Vector2 _maxStartVelocity;
        
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