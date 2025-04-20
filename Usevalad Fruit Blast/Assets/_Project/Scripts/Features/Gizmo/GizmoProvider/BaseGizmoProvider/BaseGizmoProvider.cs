using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Gizmo.GizmoProvider.BaseGizmoProvider
{
    public class BaseGizmoProvider : GizmoProvider
    {
        [SerializeField] private Color _targetColor;
        
        public Color TargetColor { get => _targetColor; set => _targetColor = value; }

        public override void Init(IFeatureConfig config)
        {
            if (config is BaseGizmoProviderConfig baseGizmoProviderConfig)
            {
                _targetColor = baseGizmoProviderConfig.TargetColor;
            }
        }

        public override Color GetGizmoColor()
        {
            return TargetColor;
        }
    }
}