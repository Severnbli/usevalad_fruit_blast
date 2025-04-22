using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver.Config
{
    [CreateAssetMenu(fileName = "CollisionResolverConfig", menuName = "Configs/Physics/Collisions/Collision Resolver Config")]
    public class CollisionResolverConfig: ScriptableObject
    {
        [SerializeField] private float _positionCorrectionPercent = 0.25f;
        [SerializeField] private float _positionCorrectionSlop = 0.1f;
        
        public float PositionCorrectionPercent => _positionCorrectionPercent;
        public float PositionCorrectionSlop => _positionCorrectionSlop;
    }
}