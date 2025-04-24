using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher
{
    public class ColliderFieldCatcher : FieldCatcher
    {
        [SerializeField] private ColliderFieldCatcherConfig _config;
        [SerializeField] private float _deathCollidersOffset = 0.5f;
        [SerializeField] private float _deathCollidersSize = 0.5f;
        
        private RectangleCollider _leftCollider;
        private RectangleCollider _rightCollider;
        private RectangleCollider _bottomCollider;
        private RectangleCollider _topDeathCollider;
        private RectangleCollider _bottomDeathCollider;
        private RectangleCollider _leftDeathCollider;
        private RectangleCollider _rightDeathCollider;
        private DynamicBody _dynamicBody;
        private Vector2 _lastCatcherSize = Vector2.zero;
        
        public ColliderFieldCatcherConfig Config { get => _config; set => _config = value; }

        public void Start()
        {
            _leftCollider = gameObject.AddComponent<RectangleCollider>();
            _rightCollider = gameObject.AddComponent<RectangleCollider>();
            _bottomCollider = gameObject.AddComponent<RectangleCollider>();
            _topDeathCollider = gameObject.AddComponent<RectangleCollider>();
            _bottomDeathCollider = gameObject.AddComponent<RectangleCollider>();
            _leftDeathCollider = gameObject.AddComponent<RectangleCollider>();
            _rightDeathCollider = gameObject.AddComponent<RectangleCollider>();
            
            _dynamicBody = gameObject.AddComponent<DynamicBody>();
            _dynamicBody.IsStatic = true;
            _dynamicBody.UseGravity = false;
            
            _topDeathCollider.IsTrigger = true;
            _bottomDeathCollider.IsTrigger = true;
            _leftDeathCollider.IsTrigger = true;
            _rightDeathCollider.IsTrigger = true;
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
            
            _lastCatcherSize = catcherSize;
            
            var halfFieldSize = _fieldProvider.GetFieldSize() / 2f;
            var halfCatcherSize = catcherSize / 2f;
            
            _leftCollider.PointA = new Vector2(-halfFieldSize.x, -halfFieldSize.y);
            _leftCollider.PointB = new Vector2(-halfCatcherSize.x, halfFieldSize.y);

            _rightCollider.PointA = new Vector2(halfCatcherSize.x, -halfFieldSize.y);
            _rightCollider.PointB = new Vector2(halfFieldSize.x, halfFieldSize.y);

            _bottomCollider.PointA = new Vector2(-halfFieldSize.x, -halfFieldSize.y);
            _bottomCollider.PointB = new Vector2(halfFieldSize.x, halfFieldSize.y - _config.Margin.Top - catcherSize.y);
            
            var pointAA = new Vector2(_leftCollider.PointA.x - _deathCollidersOffset - _deathCollidersSize,
                _leftCollider.PointA.y - _deathCollidersOffset - _deathCollidersSize);
            var pointAB = new Vector2(pointAA.x, _leftCollider.PointB.y + _deathCollidersOffset + _deathCollidersSize);
            var pointBB = new Vector2(_rightCollider.PointB.x + _deathCollidersOffset + _deathCollidersSize, 
                _rightCollider.PointB.y + _deathCollidersOffset + _deathCollidersSize);
            var pointBA = new Vector2(_rightCollider.PointB.x + _deathCollidersOffset + _deathCollidersSize,
                _rightCollider.PointA.y - _deathCollidersOffset - _deathCollidersSize);
            
            _topDeathCollider.PointA = new Vector2(pointAB.x, pointAB.y - _deathCollidersSize);
            _topDeathCollider.PointB = pointBB;
            
            _bottomDeathCollider.PointA = pointAA;
            _bottomDeathCollider.PointB = new Vector2(pointBA.x, pointBA.y + _deathCollidersSize);

            _leftDeathCollider.PointA = pointAA;
            _leftDeathCollider.PointB = new Vector2(pointAB.x + _deathCollidersSize, pointBB.y);
            
            _rightDeathCollider.PointA = new Vector2(pointBA.x - _deathCollidersSize, pointBA.y);
            _rightDeathCollider.PointB = pointBB;
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
            _bottomCollider?.SetIsTrigger(true);
        }

        public override void CloseCatcher()
        {
            _bottomCollider?.SetIsTrigger(false);
        }
    }
}