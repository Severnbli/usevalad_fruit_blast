using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver.Config;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Engine.Config
{
    [CreateAssetMenu(fileName = "PhysicsEngineConfig", menuName = "Configs/Physics/Engine/PhysicsEngineConfig")]
    public class PhysicsEngineConfig: ScriptableObject, IFeatureConfig
    {
        [SerializeField] private float _minimalBodySpeed = 0.1f;
        [SerializeField] private CollisionResolverConfig _collisionResolverConfig;
        
        public float MinimalBodySpeed => _minimalBodySpeed;
        public CollisionResolverConfig CollisionResolverConfig => _collisionResolverConfig;
    }
}