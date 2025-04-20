using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher
{
    [CreateAssetMenu(fileName = "FieldCatcherConfig", menuName = "Configs/Field Catcher Config")]
    public class ColliderFieldCatcherConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private Vector2Int _size;
        [SerializeField] private Margin _margin;
        
        public Vector2Int Size => _size;
        public Margin Margin => _margin;
    }
}