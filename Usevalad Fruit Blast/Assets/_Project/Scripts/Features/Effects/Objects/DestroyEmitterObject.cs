namespace _Project.Scripts.Features.Effects.Objects
{
    public class DestroyEmitterObject : EffectEmitterObject
    {
        private void OnDestroy()
        {
            if (!IsActive)
            {
                return;
            }
            
            Emit();
        }
    }
}