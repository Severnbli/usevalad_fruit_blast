using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Common.UI.Popups
{
    public class ScreenPopupSwitcher : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private Transform _popupTransform;

        private CancellationToken _ct;
        
        private void Awake()
        {
            _ct = gameObject.GetCancellationTokenOnDestroy();
        }

        private void Start()
        {
            _canvasGroup.gameObject.SetActive(false);
        }

        public async UniTask Show()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            _popupTransform.localScale = Vector3.zero;
            _canvasGroup.gameObject.SetActive(true);
            
            var fade = _canvasGroup
                .DOFade(1f, _animationDuration)
                .SetUpdate(true);
            var scale = _popupTransform
                .DOScale(Vector3.one, _animationDuration)
                .SetUpdate(true)
                .SetEase(Ease.OutBack);

            await UniTask.WhenAll(fade.ToUniTask(cancellationToken: _ct), scale.ToUniTask(cancellationToken: _ct));
        }
        
        public async UniTask Hide()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = true;

            var fade = _canvasGroup
                .DOFade(0f, _animationDuration)
                .SetUpdate(true);
            var scale = _popupTransform
                .DOScale(Vector3.zero, _animationDuration)
                .SetUpdate(true)
                .SetEase(Ease.InBack);

            await UniTask.WhenAll(fade.ToUniTask(cancellationToken: _ct), scale.ToUniTask(cancellationToken: _ct));

            _canvasGroup.gameObject.SetActive(false);
            _canvasGroup.blocksRaycasts = false;
        }
    }
}