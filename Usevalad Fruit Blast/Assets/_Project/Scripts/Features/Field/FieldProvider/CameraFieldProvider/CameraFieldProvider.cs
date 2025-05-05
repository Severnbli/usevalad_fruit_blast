using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldProvider.CameraFieldProvider
{
    public class CameraFieldProvider : FieldProvider, IConfigurableFeature<CameraFieldProviderConfig>
    {
        protected CameraFieldProviderConfig CameraFieldProviderConfig { get; private set; }
        
        public override Vector2 GetFieldSize()
        {
            var targetCamera = CameraFieldProviderConfig.TargetCamera;
            
            var cameraHeight = 2 * targetCamera.orthographicSize;
            var cameraWidth = cameraHeight * targetCamera.aspect;
            
            return new Vector2(cameraWidth, cameraHeight);
        }

        public override Vector2 GetFieldPosition()
        {
            var cameraPosition = CameraFieldProviderConfig.TargetCamera.transform.position;
            
            return new Vector2(cameraPosition.x, cameraPosition.y);
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

        public void Configure(CameraFieldProviderConfig cameraFieldProviderConfig)
        {
            CameraFieldProviderConfig = cameraFieldProviderConfig;
        }
    }
}