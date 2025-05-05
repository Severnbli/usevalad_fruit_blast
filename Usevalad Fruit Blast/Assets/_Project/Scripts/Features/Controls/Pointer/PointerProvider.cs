using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.Field.FieldProvider;

namespace _Project.Scripts.Features.Controls.Pointer
{
    public abstract class PointerProvider : BaseFeature
    {
        protected FieldProvider _fieldProvider;
        public bool IsEnable { get; private set; } = true;
        
        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _fieldProvider);
        }

        public void SetIsEnable(bool isEnable)
        {
            IsEnable = isEnable;
        }
    }
}