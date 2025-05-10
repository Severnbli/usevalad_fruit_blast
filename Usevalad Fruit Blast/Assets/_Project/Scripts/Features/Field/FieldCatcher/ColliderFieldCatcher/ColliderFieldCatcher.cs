using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher
{
    public class ColliderFieldCatcher : FieldCatcher, IConfigurableFeature<ColliderFieldCatcherConfig>, 
        IFixedUpdatableFeature
    {
        private RectangleCollider _leftCollider;
        private RectangleCollider _rightCollider;
        private RectangleCollider _bottomCollider;
        private RectangleCollider _topCollider;
        private DynamicBody _dynamicBody;
        
        public ColliderFieldCatcherConfig ColliderFieldCatcherConfig { get; private set; }
        
        public void Configure(ColliderFieldCatcherConfig colliderFieldCatcherConfig)
        {
            ColliderFieldCatcherConfig = colliderFieldCatcherConfig;

            ConfigureColliders();
            
            base.Configure(ColliderFieldCatcherConfig.FieldCatcherConfig);
        }

        private void ConfigureColliders()
        {
            var catcherObject = ColliderFieldCatcherConfig.CatcherObject;
            
            _leftCollider = catcherObject.AddComponent<RectangleCollider>();
            _rightCollider = catcherObject.AddComponent<RectangleCollider>();
            _bottomCollider = catcherObject.AddComponent<RectangleCollider>();
            
            _dynamicBody = catcherObject.AddComponent<DynamicBody>();
            _dynamicBody.IsStatic = true;
        }

        public void FixedUpdate()
        {
            UpdateCatcher();
        }

        public void UpdateCatcher()
        {
            UpdateCatcherPosition();
            UpdateColliders();
        }
        
        public void UpdateCatcherPosition()
        {
            ColliderFieldCatcherConfig.CatcherObject.transform.position = _fieldProvider.GetFieldPosition();
        }

        public void UpdateColliders()
        {
            var catcherSize = CalculateCatcherSize(_fieldProvider, FieldCatcherConfig);

            if (catcherSize.Equals(_lastCatcherSize))
            {
                return;
            }
            
            _lastCatcherSize = catcherSize;
            
            var halfFieldSize = _fieldProvider.GetFieldSize() / 2f;
            var halfCatcherSize = catcherSize / 2f;

            var rectangleFigure = _leftCollider.RectangleFigure;
            rectangleFigure.PointAA = new Vector2(-halfCatcherSize.x - ColliderFieldCatcherConfig.BordersWidth,
                halfFieldSize.y - FieldCatcherConfig.Margin.Top - catcherSize.y - ColliderFieldCatcherConfig.BordersWidth);
            rectangleFigure.PointBB = new Vector2(-halfCatcherSize.x,
                halfFieldSize.y + Mathf.Abs(FieldCatcherConfig.CatcherProtectHeight));
            _leftCollider.RectangleFigure = rectangleFigure;
            
            rectangleFigure.PointAA = new Vector2(halfCatcherSize.x, 
                halfFieldSize.y - FieldCatcherConfig.Margin.Top - catcherSize.y - ColliderFieldCatcherConfig.BordersWidth);
            rectangleFigure.PointBB = new Vector2(halfCatcherSize.x + ColliderFieldCatcherConfig.BordersWidth, 
                halfFieldSize.y + Mathf.Abs(FieldCatcherConfig.CatcherProtectHeight));
            _rightCollider.RectangleFigure = rectangleFigure;
            
            rectangleFigure.PointAA = new Vector2(-halfCatcherSize.x - ColliderFieldCatcherConfig.BordersWidth, 
                halfFieldSize.y - FieldCatcherConfig.Margin.Top - catcherSize.y - ColliderFieldCatcherConfig.BordersWidth);
            rectangleFigure.PointBB = new Vector2(halfCatcherSize.x + ColliderFieldCatcherConfig.BordersWidth, 
                halfFieldSize.y - FieldCatcherConfig.Margin.Top - catcherSize.y);
            _bottomCollider.RectangleFigure = rectangleFigure;
        }

        public override Vector2 GetCatcherSize()
        {
            return _lastCatcherSize;
        }

        public override Margin GetMargin()
        {
            return FieldCatcherConfig.Margin;
        }

        public override Vector2 GetSize()
        {
            return FieldCatcherConfig.Size;
        }

        public override void OpenCatcher()
        {
            _bottomCollider.IsTrigger = true;
        }

        public override void CloseCatcher()
        {
            _bottomCollider.IsTrigger = false;
        }
    }
}