using System;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Pointer.MouseProvider
{
    public class MouseProvider : PointerProvider
    {
        public event Action<Vector2> OnPrimaryMouseButtonDown;
        public event Action<Vector2> OnPrimaryMouseButtonUp;

        private void Update()
        {
            if (!_isEnabled)
            {
                return;
            }
            
            CheckPrimaryMouseButtonDown();
            CheckPrimaryMouseButtonUp();
        }

        private void CheckPrimaryMouseButtonDown()
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0))
            {
                return;
            }
            
            OnPrimaryMouseButtonDown?.Invoke(GetMousePosition());
        }

        private void CheckPrimaryMouseButtonUp()
        {
            if (!Input.GetKeyUp(KeyCode.Mouse0))
            {
                return;
            }
            
            OnPrimaryMouseButtonUp?.Invoke(GetMousePosition());
        }

        public Vector2 GetMousePosition()
        {
            return _fieldProvider.GetConvertedScreenSpacePosition(Input.mousePosition);
        }
    }
}