using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider
{
    public class CameraFieldProvider : FieldProvider
    {
        [SerializeField] private Camera _targetCamera;
        
        public Camera TargetCamera { get => _targetCamera; set => _targetCamera = value; }
        
        public override Vector2 GetFieldSize()
        {
            var cameraHeight = 2 * TargetCamera.orthographicSize;
            var cameraWidth = cameraHeight * TargetCamera.aspect;
            
            return new Vector2(cameraWidth, cameraHeight);
        }

        public override Vector2 GetFieldPosition()
        {
            return new Vector2(TargetCamera.transform.position.x, TargetCamera.transform.position.y);
        }

        public override void Init(IFeatureConfig config)
        {
            if (config is not CameraFieldProviderConfig fieldProviderConfig)
            {
                return;
            }
            
            _targetCamera = fieldProviderConfig.TargetCamera;
        }
    }
}