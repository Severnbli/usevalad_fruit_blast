using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Explosions.ExplosionProvider
{
    [CreateAssetMenu(fileName = "ExplosionProviderConfig", menuName = "Configs/Physics/Services/Explosions/Explosion Provider Config")]
    public class ExplosionProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private float _maxAffectedDistance = 0.1f;
        
        public float MaxAffectedDistance => _maxAffectedDistance;
    }
}