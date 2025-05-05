using System;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Pointer.Touch
{
    public class TouchProvider : PointerProvider, IUpdatableFeature
    {
        public event Action<Vector2> OnBeginTouch;

        public void Update()
        {
            if (!IsEnable)
            {
                return;
            }
            
            CheckBeginTouch();
        }

        private void CheckBeginTouch()
        {
            if (!TryGetTouchPhase(out var touchPhase, out var touch) || touchPhase != TouchPhase.Began)
            {
                return;
            }
            
            OnBeginTouch?.Invoke(_fieldProvider.GetConvertedScreenSpacePosition(touch.position));
        }
        
        private bool TryGetTouchPhase(out TouchPhase touchPhase, out UnityEngine.Touch touch)
        {
            if (Input.touchCount <= 0)
            {
                touchPhase = default;
                touch = default;
                return false;
            }

            touch = Input.GetTouch(0);
            
            touchPhase = touch.phase;
            return true;
        }
    }
}