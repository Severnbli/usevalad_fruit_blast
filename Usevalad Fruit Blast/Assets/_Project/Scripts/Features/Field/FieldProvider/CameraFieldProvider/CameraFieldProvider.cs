using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider
{
    public class CameraFieldProvider : FieldProvider, IConfigurableFeature<CameraFieldProviderConfig>
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

        public override Vector2 GetConvertedScreenSpacePosition(Vector2 screenSpacePosition)
        {
            var fieldSize = GetFieldSize();
            var fieldPosition = GetFieldPosition();
            
            var position = new Vector2(fieldPosition.x - fieldSize.x / 2f, fieldPosition.y - fieldSize.y / 2f);
            position.x += screenSpacePosition.x / Screen.width * fieldSize.x;
            position.y += screenSpacePosition.y / Screen.height * fieldSize.y;
            
            return position;
        }

        public void Configure(CameraFieldProviderConfig config)
        {
            _targetCamera = config.TargetCamera;   
        }
    }
}