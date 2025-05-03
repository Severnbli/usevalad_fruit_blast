using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher
{
    public class ColliderFieldCatcher : FieldCatcher
    {
        private RectangleCollider _leftCollider;
        private RectangleCollider _rightCollider;
        private RectangleCollider _bottomCollider;
        private DynamicBody _dynamicBody;
        private Vector2 _lastCatcherSize = Vector2.zero;

        public void Start()
        {
            _leftCollider = gameObject.AddComponent<RectangleCollider>();
            _rightCollider = gameObject.AddComponent<RectangleCollider>();
            _bottomCollider = gameObject.AddComponent<RectangleCollider>();
            
            _dynamicBody = gameObject.AddComponent<DynamicBody>();
            _dynamicBody.IsStatic = true;
        }

        public void FixedUpdate()
        {
            UpdateCatcher();
        }

        public void UpdateCatcher()
        {
            UpdatePosition();
            UpdateColliders();
        }
        
        public void UpdatePosition()
        {
            transform.position = FieldProvider.GetFieldPosition();
        }

        public void UpdateColliders()
        {
            var catcherSize = CalculateCatcherSize(FieldProvider, _fieldCatcherConfig);

            if (catcherSize.Equals(_lastCatcherSize))
            {
                return;
            }
            
            _lastCatcherSize = catcherSize;
            
            var halfFieldSize = FieldProvider.GetFieldSize() / 2f;
            var halfCatcherSize = catcherSize / 2f;
            
            _leftCollider.PointA = new Vector2(-halfFieldSize.x, -halfFieldSize.y);
            _leftCollider.PointB = new Vector2(-halfCatcherSize.x, 
                halfFieldSize.y + Mathf.Abs(_fieldCatcherConfig.CatcherProtectHeight));

            _rightCollider.PointA = new Vector2(halfCatcherSize.x, -halfFieldSize.y);
            _rightCollider.PointB = new Vector2(halfFieldSize.x, 
                halfFieldSize.y + Mathf.Abs(_fieldCatcherConfig.CatcherProtectHeight));

            _bottomCollider.PointA = new Vector2(-halfCatcherSize.x + 0.1f, -halfFieldSize.y);
            _bottomCollider.PointB = new Vector2(halfCatcherSize.x - 0.1f, 
                halfFieldSize.y - _fieldCatcherConfig.Margin.Top - catcherSize.y);
        }

        public override Vector2 GetCatcherSize()
        {
            return _lastCatcherSize;
        }

        public override Margin GetMargin()
        {
            return _fieldCatcherConfig.Margin;
        }

        public override Vector2 GetSize()
        {
            return _fieldCatcherConfig.Size;
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