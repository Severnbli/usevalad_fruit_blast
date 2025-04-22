using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher
{
    public class ColliderFieldCatcher : FieldCatcher
    {
        [SerializeField] private ColliderFieldCatcherConfig _config;
        
        private RectangleCollider _leftCollider;
        private RectangleCollider _rightCollider;
        private RectangleCollider _bottomCollider;
        private DynamicBody _dynamicBody;
        private Vector2 _lastCatcherSize = Vector2.zero;
        
        public ColliderFieldCatcherConfig Config { get => _config; set => _config = value; }

        public void Start()
        {
            _leftCollider = gameObject.AddComponent<RectangleCollider>();
            _rightCollider = gameObject.AddComponent<RectangleCollider>();
            _bottomCollider = gameObject.AddComponent<RectangleCollider>();
            
            _dynamicBody = gameObject.AddComponent<DynamicBody>();
            _dynamicBody.IsStatic = true;
            _dynamicBody.UseGravity = false;
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
            transform.position = _fieldProvider.GetFieldPosition();
        }

        public void UpdateColliders()
        {
            var catcherSize = GetCatcherSize();

            if (catcherSize.Equals(_lastCatcherSize))
            {
                return;
            }
            
            var halfFieldSize = _fieldProvider.GetFieldSize() / 2f;
            var halfCatcherSize = catcherSize / 2f;
            
            _leftCollider.PointA = new Vector2(-halfFieldSize.x, -halfFieldSize.y);
            _leftCollider.PointB = new Vector2(-halfCatcherSize.x, halfFieldSize.y - _config.Margin.Top);
            
            _rightCollider.PointA = new Vector2(halfCatcherSize.x, -halfFieldSize.y);
            _rightCollider.PointB = new Vector2(halfFieldSize.x, halfFieldSize.y - _config.Margin.Top);
            
            _bottomCollider.PointA = new Vector2(-halfCatcherSize.x, -halfFieldSize.y);
            _bottomCollider.PointB = new Vector2(halfCatcherSize.x, halfFieldSize.y - _config.Margin.Top - catcherSize.y);
        }

        public override Vector2 GetCatcherSize()
        {
            var fieldSize = _fieldProvider.GetFieldSize();
            
            var width = fieldSize.x - _config.Margin.Left - _config.Margin.Right;
            var heightByWidth = width * _config.Size.y / _config.Size.x;
            
            var height = fieldSize.y - _config.Margin.Top - _config.Margin.Bottom;
            var widthByHeight = height * _config.Size.x / _config.Size.y;
            
            if (_config.Size.x > _config.Size.y && heightByWidth <= height 
                || _config.Size.y > _config.Size.x && widthByHeight > width)
            {
                height = heightByWidth;
            }
            else
            {
                width = widthByHeight;
            }
            
            return new Vector2(width, height);
        }

        public override FieldProvider.FieldProvider GetFieldProvider()
        {
            return _fieldProvider;
        }

        public override Vector2 GetFieldSize()
        {
            return _fieldProvider.GetFieldSize();
        }

        public override Vector2 GetPosition()
        {
            return _fieldProvider.GetFieldPosition();
        }

        public override Margin GetMargin()
        {
            return _config.Margin;
        }

        public override Vector2 GetSize()
        {
            return _config.Size;
        }

        public override void OpenCatcher()
        {
            _bottomCollider?.SetIsCollide(false);
        }

        public override void CloseCatcher()
        {
            _bottomCollider?.SetIsCollide(true);
        }
    }
}