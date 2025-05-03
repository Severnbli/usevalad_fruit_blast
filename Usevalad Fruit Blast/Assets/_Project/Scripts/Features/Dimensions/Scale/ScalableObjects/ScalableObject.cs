using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Dimensions.Scale.ScalableObjects
{
    public class ScalableObject : MonoBehaviour
    {
        private Vector3 _baseScale;
        
        private void Start()
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
            if (!TryGetScaleProvider(out var scaleProvider))
            {
                return;
            }
            
            scaleProvider.OnChangeScale += UpdateScale;
        }

        private void DisconnectFromScaleProvider()
        {
            if (!TryGetScaleProvider(out var scaleProvider))
            {
                return;
            }
            
            scaleProvider.OnChangeScale -= UpdateScale;
        }

        private bool TryGetScaleProvider(out ScaleProvider.ScaleProvider scaleProvider)
        {
            scaleProvider = null;
            
            if (Context.TryGetComponentFromContainer(out scaleProvider))
            {
                return true;
            }
            
            Debug.LogError("Check system priority setup: scale provider must be earlier than scalable object!");
            return false;
        }

        private void UpdateScale(float scale)
        {
            transform.localScale = _baseScale * scale;
        }
    }
}