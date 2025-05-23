using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Common.UI.Popups
{
    public class DefeatPopup : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private ScreenPopupSwitcher _screenPopupSwitcher;
        
        public Button RestartButton => _restartButton;
        public ScreenPopupSwitcher ScreenPopupSwitcher => _screenPopupSwitcher;

        private void Awake()
        {
            _restartButton.onClick.AddListener(() => _screenPopupSwitcher.Hide().Forget());
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
        }
    }
}