using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Gizmo.GizmoProvider
{
    public abstract class GizmoProvider : BaseFeature
    {
        public abstract Color GetGizmoColor(); 
    }
}