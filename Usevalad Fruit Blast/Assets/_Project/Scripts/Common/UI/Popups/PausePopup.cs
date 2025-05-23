using Cysharp.Threading.Tasks;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace _Project.Scripts.Common.UI.Popups
{
    public class PausePopup : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private ScreenPopupSwitcher _screenPopupSwitcher;
            
        public Button PauseButton => _pauseButton;
        public Button ResumeButton => _resumeButton;
        public Button RestartButton => _restartButton;
        
        private void Awake()
        {
            _pauseButton.onClick.AddListener(() => _screenPopupSwitcher.Show().Forget());
            _restartButton.onClick.AddListener(() => _screenPopupSwitcher.Hide().Forget());
            _resumeButton.onClick.AddListener(() => _screenPopupSwitcher.Hide().Forget());
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveAllListeners();
            _resumeButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}