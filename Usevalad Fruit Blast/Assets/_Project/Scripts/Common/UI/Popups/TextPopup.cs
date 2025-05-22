using System;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace _Project.Scripts.Common.UI.Popups
{
    public class TextPopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private PopupAnimationType _popupAnimationType;
        [SerializeField] private float _textBaseDuration = 1f;
        [SerializeField] private float _randRotationLimit = 15f;

        private CancellationToken _ct;
        private readonly Random _random = new(); 

        [Serializable]
        public enum PopupAnimationType
        {
            Fade = 0,
            Scale = 1,
            Typewriter = 2,
            Bomb = 3
        }

        private void Awake()
        {
            _ct = gameObject.GetCancellationTokenOnDestroy();
        }

        public async UniTask AnimateText(string text, float duration, bool isRandRotation = true)
        {
            if (isRandRotation)
            {
                _text.gameObject.transform.eulerAngles = new Vector3(
                    0f,
                    0f,
                    (float) _random.NextDouble() * _randRotationLimit * 2f - _randRotationLimit
                );
            }
            else
            {
                _text.gameObject.transform.rotation = Quaternion.identity;
            }

            switch (_popupAnimationType)
            {
                case PopupAnimationType.Fade:
                {
                    await FadeAnimation(text, duration, _ct);
                    break;
                }
                case PopupAnimationType.Scale:
                {
                    await ScaleAnimation(text, duration, _ct);
                    break;
                }
                case PopupAnimationType.Typewriter:
                {
                    await TypewriterAnimation(text, duration, _ct);
                    break;
                }
                case PopupAnimationType.Bomb:
                {
                    await BombAnimation(text, duration, _ct);
                    break;
                }
            }

            _text.text = string.Empty;
        }

        public async UniTask AnimateTexts(string[] texts, float duration = 0f)
        {
            float singleDuration;
            
            if (duration == 0f)
            {
                singleDuration = _textBaseDuration;
            }
            else
            {
                singleDuration = duration / Mathf.Max(texts.Length, 1);
            }

            foreach (var text in texts)
            {
                await AnimateText(text, singleDuration);
            }
        }

        private async UniTask FadeAnimation(string newText, float duration, CancellationToken token)
        {
            var half = duration / 2f;

            await _text.DOFade(0f, half)
                .SetEase(Ease.InOutSine)
                .WithCancellation(token);

            _text.text = newText;

            await _text.DOFade(1f, half)
                .SetEase(Ease.InOutSine)
                .WithCancellation(token);
        }

        private async UniTask ScaleAnimation(string newText, float duration, CancellationToken token)
        {
            var half = duration / 2f;

            await _text.transform.DOScale(Vector3.zero, half)
                .SetEase(Ease.InCubic)
                .WithCancellation(token);

            _text.text = newText;

            await _text.transform.DOScale(Vector3.one, half)
                .SetEase(Ease.OutCubic)
                .WithCancellation(token);
        }

        private async UniTask TypewriterAnimation(string newText, float duration, CancellationToken token)
        {
            var builder = new StringBuilder();
            var delay = duration / Mathf.Max(newText.Length, 1);

            foreach (var c in newText)
            {
                builder.Append(c);
                _text.text = builder.ToString();
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            }
        }

        private async UniTask BombAnimation(string newText, float duration, CancellationToken token)
        {
            var scaleUpDuration = duration * 0.2f;
            var scaleDownDuration = duration * 0.05f;
            var holdDuration = duration * 0.75f;

            await _text.DOFade(0f, scaleUpDuration * 0.3f)
                .SetEase(Ease.InQuad)
                .WithCancellation(token);

            _text.text = newText;

            _text.alpha = 1f;
            _text.transform.localScale = Vector3.one;

            await _text.transform.DOScale(1.1f, scaleUpDuration)
                .SetEase(Ease.OutBack)
                .WithCancellation(token);

            await _text.transform.DOScale(1f, scaleDownDuration)
                .SetEase(Ease.InOutSine)
                .WithCancellation(token);

            await UniTask.Delay(TimeSpan.FromSeconds(holdDuration), cancellationToken: token);
        }
    }
}
