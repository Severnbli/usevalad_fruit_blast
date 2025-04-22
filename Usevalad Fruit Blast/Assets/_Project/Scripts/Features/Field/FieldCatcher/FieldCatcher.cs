using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.Common;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher
{
    public abstract class FieldCatcher : BaseFeature
    {
        [SerializeField] protected FieldProvider.FieldProvider _fieldProvider;
        
        public FieldProvider.FieldProvider FieldProvider { get => _fieldProvider; set => _fieldProvider = value; }

        public override void Init(IFeatureConfig config) {}

        public abstract FieldProvider.FieldProvider GetFieldProvider();
        public abstract Vector2 GetPosition();
        public abstract Margin GetMargin();
        public abstract Vector2 GetSize();
        public abstract Vector2 GetCatcherSize();
        public abstract Vector2 GetFieldSize();
        public abstract void OpenCatcher();
        public abstract void CloseCatcher();
    }
}