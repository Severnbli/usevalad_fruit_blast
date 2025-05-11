using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.SplitSpriteEffectProvider
{
    [CreateAssetMenu(fileName = "SplitSpriteEffectProviderConfig", menuName = "Configs/Effects/Providers/Split Sprite Effect Provider Config")]
    public class SplitSpriteEffectProviderConfig : EffectProviderConfig
    {
        [SerializeField] private Vector2 _minStartVelocity = Vector2.zero;
        [SerializeField] private Vector2 _maxStartVelocity = Vector2.zero;
        [SerializeField] private int _uniqueSplitsQuantity = 3;
        [SerializeField] private float _spritesSafeAreaPercentage = 0.2f;
        
        public Vector2 MinStartVelocity => _minStartVelocity;
        public Vector2 MaxStartVelocity => _maxStartVelocity;
        public int UniqueSplitsQuantity => _uniqueSplitsQuantity;
        public float SpritesSafeAreaPercentage => _spritesSafeAreaPercentage;
    }
}