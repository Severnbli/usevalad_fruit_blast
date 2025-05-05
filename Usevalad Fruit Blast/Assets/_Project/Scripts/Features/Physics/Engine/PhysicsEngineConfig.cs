using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Engine
{
    [CreateAssetMenu(fileName = "PhysicsEngineConfig", menuName = "Configs/Physics/Engine/Physics Engine Config")]
    public class PhysicsEngineConfig: ScriptableObject, IFeatureConfig
    {
        [SerializeField] private float _minBodySpeed = 0.1f;
        [SerializeField] private float _maxBodySpeed = 100f;
        [SerializeField] private CollisionResolverConfig _collisionResolverConfig;
        
        public float MinBodySpeed => _minBodySpeed;
        public float MaxBodySpeed => _maxBodySpeed;
        public CollisionResolverConfig CollisionResolverConfig => _collisionResolverConfig;
    }
}