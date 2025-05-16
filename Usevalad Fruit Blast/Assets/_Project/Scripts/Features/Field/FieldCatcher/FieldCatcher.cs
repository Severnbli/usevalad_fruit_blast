using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher
{
    public abstract class FieldCatcher : BaseFeature, IConfigurableFeature<FieldCatcherConfig>
    {
        protected FieldProvider.FieldProvider _fieldProvider;
        protected Vector2 _lastCatcherSize = Vector2.zero;
        
        public FieldCatcherConfig FieldCatcherConfig { get; private set; }
        
        public override void Init()
        {
            Context.TryGetComponentFromContainer(out _fieldProvider);
        }
        
        public void Configure(FieldCatcherConfig fieldCatcherConfig)
        {
            FieldCatcherConfig = fieldCatcherConfig;
            UpdateCatcher();
        }

        public static Vector2 CalculateCatcherSize(FieldProvider.FieldProvider fieldProvider, FieldCatcherConfig fieldCatcherConfig)
        {
            var fieldSize = fieldProvider.GetFieldSize();
            
            var width = fieldSize.x - fieldCatcherConfig.Margin.Left - fieldCatcherConfig.Margin.Right;
            var heightByWidth = width * fieldCatcherConfig.Size.y / fieldCatcherConfig.Size.x;
            
            var height = fieldSize.y - fieldCatcherConfig.Margin.Top - fieldCatcherConfig.Margin.Bottom;
            var widthByHeight = height * fieldCatcherConfig.Size.x / fieldCatcherConfig.Size.y;
            
            if (fieldCatcherConfig.Size.x > fieldCatcherConfig.Size.y && heightByWidth <= height 
                || fieldCatcherConfig.Size.y > fieldCatcherConfig.Size.x && widthByHeight > width)
            {
                height = heightByWidth;
            }
            else
            {
                width = widthByHeight;
            }
            
            return new Vector2(width, height);
        }

        public abstract void UpdateCatcher();

        public FieldProvider.FieldProvider GetFieldProvider()
        {
            return _fieldProvider;
        }
        
        public Vector2 GetPosition()
        {
            return _fieldProvider.GetFieldPosition();
        }
        
        public abstract Margin GetMargin();
        public abstract Vector2 GetSize();
        
        public float GetArea()
        {
            return _lastCatcherSize.x * _lastCatcherSize.y;
        }
        
        public abstract Vector2 GetCatcherSize();
        public abstract void OpenCatcher();
        public abstract void CloseCatcher();
    }
}