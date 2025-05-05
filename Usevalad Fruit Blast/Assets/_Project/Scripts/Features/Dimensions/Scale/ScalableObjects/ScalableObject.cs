using _Project.Scripts.Bootstrap;
using _Project.Scripts.Common.Finders;
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
            if (!ObjectFinder.TryFindObjectByType(out SystemCoordinator systemCoordinator))
            {
                return;
            }
            
            var context = systemCoordinator.Context;

            if (!context.TryGetComponentFromContainer(out _scaleProvider))
            {
                return;
            }
            
            _scaleProvider.OnChangeScale += UpdateScale;
            UpdateScale(_scaleProvider.Scale);
        }

        private void DisconnectFromScaleProvider()
        {
            if (_scaleProvider == null)
            {
                return;
            }
            
            _scaleProvider.OnChangeScale -= UpdateScale;
        }

        private void UpdateScale(float scale)
        {
            transform.localScale = _baseScale * scale;
        }
    }
}