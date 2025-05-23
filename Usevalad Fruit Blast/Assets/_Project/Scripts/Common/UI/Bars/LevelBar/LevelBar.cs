using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Common.UI.Bars.LevelBar
{
    public class LevelBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        
        private int _lastLevel;
        
        public event Action<int> OnLevelChanged;
        public event Action<int> OnLevelUp;
        public event Action<int> OnLevelDown;
        
        public int Level => _lastLevel; 
        
        public void Setup(int initialLevel)
        {
            _lastLevel = initialLevel;
            PrintLevel();
        }

        public void LevelUp()
        {
            SetLevel(_lastLevel + 1);
        }

        public void LevelDown()
        {
            SetLevel(_lastLevel - 1);
        }

        public void SetLevel(int level)
        {
            if (level == _lastLevel)
            {
                return;
            }
            
            OnLevelChanged?.Invoke(level);

            if (level < _lastLevel)
            {
                OnLevelDown?.Invoke(level);
            }
            else
            {
                OnLevelUp?.Invoke(level);
            }
            
            _lastLevel = level;
            
            PrintLevel();
        }

        private void PrintLevel()
        {
            _levelText.text = _lastLevel.ToString();
        }
    }
}