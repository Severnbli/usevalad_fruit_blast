using _Project.Scripts.Common.Repositories;

namespace _Project.Scripts.Features.FeatureCore
{
    public abstract class BaseFeature
    {
        public Context<BaseFeature> Context { get; private set; }

        public virtual void Init() {}

        public void SetContext(Context<BaseFeature> context)
        {
            Context = context;
        }
    }
}