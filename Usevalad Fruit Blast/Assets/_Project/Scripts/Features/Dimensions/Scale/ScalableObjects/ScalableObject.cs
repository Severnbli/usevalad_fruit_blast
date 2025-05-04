using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Dimensions.Scale.ScalableObjects
{
    public class ScalableObject : MonoBehaviour
    {
        private Vector3 _baseScale;
        
        private void OnEnable()
        {
            SetBaseScale(transform.localScale);
            ConnectToScaleProvider();
        }
        
        private void OnDestroy()
        {
            DisconnectFromScaleProvider();
        }

        private void SetBaseScale(Vector3 baseScale)
        {
            _baseScale = baseScale;
        }

        private void ConnectToScaleProvider()
        {
            if (!Context.TryGetComponentFromContainer(out ScaleProvider.ScaleProvider scaleProvider))
            {
                Debug.LogError("Check system priority setup: scale provider must be earlier than scalable object!");
                return;
            }
            
            scaleProvider.OnChangeScale += UpdateScale;
        }

        private void DisconnectFromScaleProvider()
        {
            if (!Context.TryGetComponentFromContainer(out ScaleProvider.ScaleProvider scaleProvider))
            {
                return;
            }
            
            scaleProvider.OnChangeScale -= UpdateScale;
        }

        private void UpdateScale(float scale)
        {
            transform.localScale = _baseScale * scale;
        }
    }
}