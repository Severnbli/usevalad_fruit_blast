using _Project.Scripts.Common.Objects;
using _Project.Scripts.Features.Stats.Experience;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.ExperienceEffectProvider
{
    public class ExperienceEffectObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private int _experience;
        private Transform _target;
        private ExperienceProvider _experienceProvider;
        private BezierMover _bezierMover;
        
        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public void Setup(int experience, Transform target, ExperienceProvider experienceProvider)
        {
            _experience = experience;
            _target = target;
            _experienceProvider = experienceProvider;
        }
        
        private void Awake()
        {
            if (gameObject.TryGetComponent(out _bezierMover))
            {
                _bezierMover.OnEndMoving += CompleteExperience;
            }
        }

        private void CompleteExperience()
        {
            _experienceProvider.AddExperience(_experience);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (_bezierMover != null)
            {
                _bezierMover.OnEndMoving -= CompleteExperience;
            }
        }
    }
}
