using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Common.UI.Popups
{
    public class PerkSelectionPopup : MonoBehaviour
    {
        [SerializeField] private ScreenPopupSwitcher _screenPopupSwitcher;
        [SerializeField] private TextMeshProUGUI _leftPopupText;
        [SerializeField] private Image _leftPopupImage;
        [SerializeField] private Button _leftPopupButton;
        [SerializeField] private TextMeshProUGUI _rightPopupText;
        [SerializeField] private Image _rightPopupImage;
        [SerializeField] private Button _rightPopupButton;
        [SerializeField] private TextMeshProUGUI _levelText;
        
        public ScreenPopupSwitcher ScreenPopupSwitcher => _screenPopupSwitcher;
        public TextMeshProUGUI LeftPopupText => _leftPopupText;
        public Image LeftPopupImage => _leftPopupImage;
        public Button LeftPopupButton => _leftPopupButton;
        public TextMeshProUGUI RightPopupText => _rightPopupText;
        public Image RightPopupImage => _rightPopupImage;
        public Button RightPopupButton => _rightPopupButton;
        public TextMeshProUGUI LevelText => _levelText;

        private void Awake()
        {
            _leftPopupButton.onClick.AddListener(() => _screenPopupSwitcher.Hide().Forget());
            _rightPopupButton.onClick.AddListener(() => _screenPopupSwitcher.Hide().Forget());
        }

        private void OnDestroy()
        {
            _leftPopupButton.onClick.RemoveAllListeners();
            _rightPopupButton.onClick.RemoveAllListeners();
        }
    }
}