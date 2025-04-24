using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Pointer.Mouse.MouseProvider
{
    public class MouseProvider : PointerProvider
    {
        [SerializeField] protected FieldProvider _fieldProvider;
        
        public FieldProvider FieldProvider => _fieldProvider;
        
        public bool TryGetMouseDownPosition(out Vector2 touchPosition)
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0))
            {
                touchPosition = default;
                return false;
            }
            
            touchPosition = GetMousePosition();
            return true;
        }
        
        public bool TryGetMouseUpPosition(out Vector2 touchPosition)
        {
            if (!Input.GetKeyUp(KeyCode.Mouse0))
            {
                touchPosition = default;
                return false;
            }
            
            touchPosition = GetMousePosition();
            return true;
        }

        public Vector2 GetMousePosition()
        {
            return _fieldProvider.GetConvertedScreenSpacePosition(Input.mousePosition);
        }
        
        public override void Init(IFeatureConfig config) {}
    }
}