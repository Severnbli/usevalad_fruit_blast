using UnityEngine;
using DG.Tweening;

namespace _Project.Scripts.Features.Effects.Providers.ExplosionEffectProvider
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ExplosionEffectObject : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        [SerializeField] private float flyDistance = 1.5f;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Sprite sprite, float totalLifetime)
        {
            _renderer.sprite = sprite;
            _renderer.color = new Color(1, 1, 1, 0);

            var flyDuration = totalLifetime * 0.6f;
            var fadeDuration = totalLifetime * 0.4f;

            var direction = UnityEngine.Random.insideUnitCircle.normalized;
            var targetPos = (Vector2)transform.position + direction * flyDistance;

            var sequence = DOTween.Sequence();

            sequence.Append(_renderer.DOFade(1f, 0.1f));
            sequence.Join(transform.DOMove(targetPos, flyDuration).SetEase(Ease.OutQuad));
            sequence.AppendInterval(totalLifetime - flyDuration - fadeDuration);
            sequence.Append(_renderer.DOFade(0f, fadeDuration).SetEase(Ease.InQuad));
            sequence.OnComplete(() => Destroy(gameObject));
            sequence.SetAutoKill(true);
        }
    }
}