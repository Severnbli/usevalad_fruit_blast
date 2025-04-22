using System;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.Gizmo
{
    public class FieldCatcherDrawer: MonoBehaviour
    {
        [SerializeField] private FieldCatcher _fieldCatcher;
        [SerializeField] private Color _gizmoColor = Color.yellow;

        private void OnDrawGizmos()
        {
            try
            {
                var halfCatcherSize = _fieldCatcher.GetCatcherSize() / 2f;
                var halfFieldSize = _fieldCatcher.GetFieldSize() / 2f;
                var position = _fieldCatcher.GetPosition();
                var margin = _fieldCatcher.GetMargin();
                
                var rightUpPoint = new Vector2(position.x + halfCatcherSize.x, 
                    position.y + halfFieldSize.y - margin.Top);
                var leftDownPoint = new Vector2(position.x - halfCatcherSize.x,
                    position.y + halfFieldSize.y - margin.Top - halfCatcherSize.y * 2f);
                var rightDownPoint = new Vector2(rightUpPoint.x, leftDownPoint.y);
                var leftUpPoint = new Vector2(leftDownPoint.x, rightUpPoint.y);
                
                Gizmos.color = _gizmoColor;
                
                Gizmos.DrawLine(rightUpPoint, rightDownPoint);
                Gizmos.DrawLine(rightUpPoint, leftUpPoint);
                Gizmos.DrawLine(leftDownPoint, rightDownPoint);
                Gizmos.DrawLine(leftDownPoint, leftUpPoint);
            }
            catch (Exception)
            {
                ;
            }
        }
    }
}