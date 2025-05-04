using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldProvider;
using _Project.Scripts.System;
using _Project.Scripts.System.Logs.Logger;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Pointer
{
    public class PointerProvider : BaseFeature
    {
        [SerializeField] protected FieldProvider _fieldProvider;
        [SerializeField] protected bool _isEnabled;
        
        public FieldProvider FieldProvider => _fieldProvider;
        public bool IsEnabled => _isEnabled;
        
        public override void Init()
        {
            base.Init();

            if (!Context.TryGetComponentFromContainer(out FieldProvider fieldProvider))
            {
                LogManager.RegisterLogMessage(LogManager.LogType.Error, LogMessages.DependencyNotFound(
                    GetType().ToString(), fieldProvider.GetType().ToString()));
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