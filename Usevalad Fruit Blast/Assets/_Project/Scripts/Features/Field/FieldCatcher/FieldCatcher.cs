using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.Common;
using _Project.Scripts.System;
using _Project.Scripts.System.Logs;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher
{
    public abstract class FieldCatcher : BaseFeature, IConfigurableFeature<FieldCatcherConfig>
    {
        [SerializeField] protected FieldCatcherConfig _fieldCatcherConfig;
        
        public FieldCatcherConfig FieldCatcherConfig => _fieldCatcherConfig;
        public FieldProvider.FieldProvider FieldProvider { get; protected set; }
        
        protected Vector2 _lastCatcherSize = Vector2.zero;

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
        
        public virtual Vector2 GetPosition()
        {
            return FieldProvider.GetFieldPosition();
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

        public override void Init()
        {
            if (!Context.TryGetComponentFromContainer(out FieldProvider.FieldProvider fieldProvider))
            {
                Debug.LogError(LogMessages.DependencyNotFound(GetType().ToString(), 
                    fieldProvider.GetType().ToString()));
                return;
            }
            
            FieldProvider = fieldProvider;
        }
        
        public void Configure(FieldCatcherConfig config)
        {
            _fieldCatcherConfig = config;
        }
    }
}