using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher
{
    [CreateAssetMenu(fileName = "ColliderFieldCatcherConfig", menuName = "Configs/Collider Field Catcher Config")]
    public class ColliderFieldCatcherConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private Margin _margin;
        
        public Vector2 Size => _size;
        public Margin Margin => _margin;
    }
}