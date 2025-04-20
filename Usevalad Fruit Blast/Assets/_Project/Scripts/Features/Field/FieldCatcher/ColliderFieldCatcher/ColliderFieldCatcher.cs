using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher
{
    public class ColliderFieldCatcher : FieldCatcher
    {
        [SerializeField] private Margin _margin;
        [SerializeField] private Vector2 _size;
        
        private GameObject _fieldCatcher;
        private RectangleCollider _leftCollider;
        private RectangleCollider _rightCollider;
        private RectangleCollider _bottomCollider;
        
        public Margin Margin { get => _margin; set => _margin = value; }
        public Vector2 Size { get => _size; set => _size = value; }
        
        public static readonly string FieldCatcherName = "FieldCatcher";

        public void InitCatcher()
        {
            _fieldCatcher = new(FieldCatcherName);
            _leftCollider = _fieldCatcher.AddComponent<RectangleCollider>();
            _rightCollider = _fieldCatcher.AddComponent<RectangleCollider>();
            _bottomCollider = _fieldCatcher.AddComponent<RectangleCollider>();
        }
        
        public void UpdateCatcher()
        {
            UpdatePosition();
            UpdateColliders();
        }
        
        public void UpdatePosition()
        {
            _fieldCatcher.transform.position = _fieldProvider.GetFieldPosition();
        }

        public void UpdateColliders()
        {
            var fieldSize = _fieldProvider.GetFieldSize();
            
            var width = fieldSize.x - Margin.Left - Margin.Right;
            var heightByWidth = width * Size.y / Size.x;
            
            var height = fieldSize.y - Margin.Top - Margin.Bottom;
            var widthByHeight = height * Size.x / Size.y;
            
            if (Size.x > Size.y && heightByWidth <= height || Size.y > Size.x && widthByHeight > width)
            {
                height = heightByWidth;
            }
            else
            {
                width = widthByHeight;
            }
            
            var halfFieldSize = fieldSize / 2f;
            var halfCatcherSize = new Vector2(width, height) / 2f;
            
            _leftCollider.PointA = new Vector2(-halfFieldSize.x, -halfFieldSize.y);
            _leftCollider.PointB = new Vector2(-halfCatcherSize.x, halfFieldSize.y - _margin.Top);
            
            _rightCollider.PointA = new Vector2(halfCatcherSize.x, -halfFieldSize.y);
            _rightCollider.PointB = new Vector2(halfFieldSize.x, halfFieldSize.y - _margin.Top);
            
            _bottomCollider.PointA = new Vector2(-halfCatcherSize.x, -halfFieldSize.y);
            _bottomCollider.PointB = new Vector2(halfCatcherSize.x, halfFieldSize.y - _margin.Top - height);
        }
        
        public override void Init(IFeatureConfig config)
        {
            base.Init(config);
            
            if (config is not ColliderFieldCatcherConfig colliderFieldCatcherConfig)
            {
                return;
            }
            
            _margin = colliderFieldCatcherConfig.Margin;
            _size = colliderFieldCatcherConfig.Size;
            
            InitCatcher();
            UpdateCatcher();
        }

        public override FieldProvider.FieldProvider GetFieldProvider()
        {
            return _fieldProvider;
        }

        public override Vector2 GetPosition()
        {
            return _fieldProvider.GetFieldPosition();
        }

        public override Margin GetMargin()
        {
            return _margin;
        }

        public override Vector2 GetSize()
        {
            return _size;
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