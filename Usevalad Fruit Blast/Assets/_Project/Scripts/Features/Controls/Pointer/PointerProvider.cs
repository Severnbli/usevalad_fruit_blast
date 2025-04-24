using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Pointer
{
    public class PointerProvider : BaseFeature
    {
        [SerializeField] protected FieldProvider _fieldProvider;
        
        public FieldProvider FieldProvider => _fieldProvider;
        
        public override void Init(IFeatureConfig config) {}
    }
}