using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher
{
    [CreateAssetMenu(fileName = "ColliderFieldCatcherConfig", menuName = "Configs/Field/Field Catcher/Collider Field Catcher Config")]
    public class ColliderFieldCatcherConfig : FieldCatcherConfig
    {
        [SerializeField] private float _bordersWidth = 2f;
        
        public float BordersWidth => _bordersWidth;
    }
}