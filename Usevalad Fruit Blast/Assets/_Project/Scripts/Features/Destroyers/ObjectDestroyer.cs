using _Project.Scripts.Features.Common;

namespace _Project.Scripts.Features.Destroyers
{
    public abstract class ObjectDestroyer : BaseFeature
    {
        protected void Update()
        {
            CheckDestroyCondition();
        }

        protected abstract void CheckDestroyCondition();

        public override void Init(IFeatureConfig config) {}
    }
}