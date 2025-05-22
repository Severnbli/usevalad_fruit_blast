using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects.Conditional
{
    public class DestroyEmitterObject : ConditionalEmitterObject
    {
        [SerializeField] private bool _isActive;
        
        public bool IsActive { get => _isActive; set => _isActive = value; }
        
        private void OnDestroy()
        {
            if (!IsActive)
            {
                return;
            }
            
            EmitAll();
        }
    }
}