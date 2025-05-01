using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver.Config;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Engine.Config
{
    [CreateAssetMenu(fileName = "PhysicsEngineConfig", menuName = "Configs/Physics/Engine/PhysicsEngineConfig")]
    public class PhysicsEngineConfig: ScriptableObject, IFeatureConfig
    {
        [SerializeField] private float _minBodySpeed = 0.1f;
        [SerializeField] private float _maxBodySpeed = 1000f;
        [SerializeField] private int _collisionResolvingIterations = 6;
        [SerializeField] private CollisionResolverConfig _collisionResolverConfig;
        
        public float MinBodySpeed => _minBodySpeed;
        public float MaxBodySpeed => _maxBodySpeed;
        public int CollisionResolvingIterations => _collisionResolvingIterations;
        public CollisionResolverConfig CollisionResolverConfig => _collisionResolverConfig;
    }
}