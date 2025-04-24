using _Project.Scripts.Features.Spawners.Config;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Features.Spawners.PhysicsObjectSpawner.Config
{
    [CreateAssetMenu(fileName = "PhysicsGroupObjectConfig", menuName = "Configs/Spawners/Physics Object Spawner/Physicsc Group Object Config")]
    public class PhysicsGroupObjectConfig : ScriptableObject
    {
        [SerializeField] private ObjectConfig[] _objectConfig;
        [SerializeField] private float _minScale = 1f;
        [SerializeField] private float _maxScale = 1f;
        [SerializeField] private float _minMass = 1f;
        [SerializeField] private float _maxMass = 1f;
        [SerializeField] private Vector2 _minStartVector = Vector2.zero;
        [SerializeField] private Vector2 _maxStartVector = Vector2.zero;
        [SerializeField] private float _minStartSpeed = 1f;
        [SerializeField] private float _maxStartSpeed = 1f;
        
        public ObjectConfig[] ObjectConfig => _objectConfig;
        public float MinScale => _minScale;
        public float MaxScale => _maxScale;
        public float MinMass => _minMass;
        public float MaxMass => _maxMass;
        public Vector2 MinStartVector => _minStartVector;
        public Vector2 MaxStartVector => _maxStartVector;
        public float MinStartSpeed => _minStartSpeed;
        public float MaxStartSpeed => _maxStartSpeed;
    }
}