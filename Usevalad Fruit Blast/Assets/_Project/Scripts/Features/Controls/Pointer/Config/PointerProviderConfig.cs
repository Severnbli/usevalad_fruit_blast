using System;
using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Pointer.Config
{
    [Serializable]
    public class PointerProviderConfig : IFeatureConfig
    {
        [SerializeField] private FieldProvider _fieldProvider;
        
        public FieldProvider FieldProvider => _fieldProvider;
    }
}