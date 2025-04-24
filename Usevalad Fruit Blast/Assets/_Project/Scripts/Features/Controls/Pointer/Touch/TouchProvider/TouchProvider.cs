using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Pointer.Touch.TouchProvider
{
    public class TouchProvider : PointerProvider
    {
        public bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount <= 0)
            {
                touchPosition = default;
                return false;
            }

            var touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Began)
            {
                touchPosition = default;
                return false;
            }

            touchPosition = _fieldProvider.GetConvertedScreenSpacePosition(touch.position);
            return true;
        }
        
        public override void Init(IFeatureConfig config) {}
    }
}