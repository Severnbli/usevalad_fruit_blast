using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExplosionEffectProvider
{
    [CreateAssetMenu(fileName = "ExplosionEffectProviderConfig", menuName = "Configs/Effects/Providers/Explosion Effect Provider Config")]
    public class ExplosionEffectProviderConfig : EffectProviderConfig
    {
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private Sprite[] _explosionSprites;
        [SerializeField] private float _explosionDuration;
        [SerializeField] private int _minExplosionsCount;
        [SerializeField] private int _maxExplosionsCount;

        public GameObject ExplosionPrefab => _explosionPrefab;
        public Sprite[] ExplosionSprites => _explosionSprites;
        public float ExplosionDuration => _explosionDuration;
        public int MinExplosionsCount => _minExplosionsCount;
        public int MaxExplosionsCount => _maxExplosionsCount;
    }
}