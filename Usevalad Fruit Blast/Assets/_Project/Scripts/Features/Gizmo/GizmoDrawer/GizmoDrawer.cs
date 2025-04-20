using _Project.Scripts.Features.Common;
using _Project.Scripts.System;
using UnityEngine;

namespace _Project.Scripts.Features.Gizmo.GizmoDrawer
{
    public abstract class GizmoDrawer : BaseFeature
    {
        [SerializeField] protected GizmoProvider.GizmoProvider _gizmoProvider;
        
        public GizmoProvider.GizmoProvider GizmoProvider { get => _gizmoProvider; set => _gizmoProvider = value; }

        public override void Init(IFeatureConfig config)
        {
            _gizmoProvider = Context.Container.GetComponent<GizmoProvider.GizmoProvider>();
            
            if (_gizmoProvider == null)
            {
                Debug.LogError("Check system priority setup: gizmo provider must be earlier than gizmo drawer!");
            }
        }
    }
}