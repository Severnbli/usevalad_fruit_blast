using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldProvider;
using _Project.Scripts.System;
using _Project.Scripts.System.Logs;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Pointer
{
    public class PointerProvider : BaseFeature
    {
        [SerializeField] protected FieldProvider _fieldProvider;
        [SerializeField] protected bool _isEnabled = true;
        
        public FieldProvider FieldProvider => _fieldProvider;
        public bool IsEnabled => _isEnabled;
        
        public override void Init()
        {
            base.Init();

            if (!Context.TryGetComponentFromContainer(out FieldProvider fieldProvider))
            {
                Debug.LogError(LogMessages.DependencyNotFound(GetType().ToString(), 
                    fieldProvider.GetType().ToString()));
                return;
            }
            
            _fieldProvider = fieldProvider;
        }

        public void SetIsEnable(bool isEnable)
        {
            _isEnabled = isEnable;
        }
    }
}