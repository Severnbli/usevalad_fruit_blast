using _Project.Scripts.Features.Controls.Pointer.Mouse.MouseProvider;
using _Project.Scripts.Features.Controls.Pointer.Touch.TouchProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Destroyers.ClickObjectDestroyer
{
    public class ClickObjectDestroyer : ObjectDestroyer
    {
        [SerializeField] private TouchProvider _touchProvider;
        [SerializeField] private MouseProvider _mouseProvider;
        [SerializeField] private float _destroyDistance;
        
        public TouchProvider TouchProvider => _touchProvider;
        public MouseProvider MouseProvider => _mouseProvider;
        
        protected override void CheckDestroyCondition()
        {
# if UNITY_ANDROID
            CheckTouch();
# else
            CheckMousePosition();
#endif
        }

        private void CheckTouch()
        {
            if (!_touchProvider.TryGetTouchPosition(out var touchPosition))
            {
                return;
            }
            
            DestroyObjectAt(touchPosition);
        }

        private void CheckMouseClick()
        {
            if (!_mouseProvider.TryGetMouseDownPosition(out var touchPosition))
            {
                return;
            }
            
            DestroyObjectAt(touchPosition);
        }

        private void DestroyObjectAt(Vector2 position)
        {
            // TODO: search for nearest destroyable object in the area and run destroy method
        }
    }
}