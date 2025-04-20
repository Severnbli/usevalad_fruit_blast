using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Gizmo.GizmoProvider.BaseGizmoProvider
{
    [CreateAssetMenu(fileName = "BaseGizmoProvider", menuName = "Configs/Base Gizmo Provider")]
    public class BaseGizmoProviderConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private Color _targetColor;
        
        public Color TargetColor { get => _targetColor; set => _targetColor = value; }
    }
}