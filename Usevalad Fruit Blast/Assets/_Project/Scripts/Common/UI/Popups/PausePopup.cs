using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace _Project.Scripts.Common.UI.Popups
{
    public class PausePopup : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationDuration = 0.2f;
        [SerializeField] private Transform _popupTransform;
        
        private CancellationToken _ct;

        public Button PauseButton => _pauseButton;
        public Button ResumeButton => _resumeButton;
        public Button RestartButton => _restartButton;
        
        private void Awake()
        {
            _ct = gameObject.GetCancellationTokenOnDestroy();
            
            _pauseButton.onClick.AddListener(() => Show().Forget());
            _restartButton.onClick.AddListener(() => Hide().Forget());
            _resumeButton.onClick.AddListener(() => Hide().Forget());
            
            _canvasGroup.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveAllListeners();
            _resumeButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }
 
        public async UniTask Show()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
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
            
            _canvasGroup.interactable = true;
        }
        
        public async UniTask Hide()
        {
            _canvasGroup.interactable = true;
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
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}