using _Project.Scripts.Common.Dimensions;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher
{
    [CreateAssetMenu(fileName = "FieldCatcherConfig", menuName = "Configs/Field/Field Catcher/Field Catcher Config")]
    public class FieldCatcherConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private Margin _margin;
        [SerializeField] private float _catcherProtectHeight = 2f;
        
        public Vector2 Size => _size;
        public Margin Margin => _margin;
        public float CatcherProtectHeight => _catcherProtectHeight;
    }
}