using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldProvider;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Pointer
{
    public class PointerProvider : BaseFeature
    {
        [SerializeField] protected FieldProvider _fieldProvider;
        
        public FieldProvider FieldProvider => _fieldProvider;
        
        public override void Init()
        {
            base.Init();

            if (!Context.TryGetComponentFromContainer(out FieldProvider fieldProvider))
            {
                Debug.LogError("Check system priority setup: field provider must be earlier than pointer provider!");
                return;
            }
            
            _fieldProvider = fieldProvider;
        }
    }
}