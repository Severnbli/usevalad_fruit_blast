using _Project.Scripts.System;
using _Project.Scripts.System.Logs.Logger;
using UnityEngine;

namespace _Project.Scripts.Features.Dimensions.Scale.ScalableObjects
{
    public class ScalableObject : MonoBehaviour
    {
        private Vector3 _baseScale;
        private ScaleProvider.ScaleProvider _scaleProvider;
        
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
            if (!Context.TryGetComponentFromContainer(out _scaleProvider))
            {
                LogManager.RegisterLogMessage(LogManager.LogType.Error, LogMessages.DependencyNotFound(
                    GetType().ToString(), _scaleProvider.GetType().ToString()));
                return;
            }
            
            _scaleProvider.OnChangeScale += UpdateScale;
            UpdateScale(_scaleProvider.Scale);
        }

        private void DisconnectFromScaleProvider()
        {
            if (_scaleProvider != null)
            {
                _scaleProvider.OnChangeScale -= UpdateScale;
            }
        }

        private void UpdateScale(float scale)
        {
            transform.localScale = _baseScale * scale;
        }
    }
}